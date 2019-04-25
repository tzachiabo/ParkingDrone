import cv2
import boto3
import argparse
from functools import reduce

BUCKET_NAME = "images-for-car-plate-detection"
PIC_NAME = "big.JPG"
CAR_PIC_NAME = "car.JPG"

parser = argparse.ArgumentParser()
parser.add_argument("-i", "--image", type=str, required=True, help="path to image")
args = vars(parser.parse_args())


def upload_image(image_path, sever_pic_name):
    s3 = boto3.client('s3')
    with open(image_path, "rb") as f:
        s3.upload_fileobj(f, BUCKET_NAME, sever_pic_name)


def detect_car_plate_number(car_img_path):
    upload_image(car_img_path, CAR_PIC_NAME)
    client = boto3.client('rekognition')
    response = client.detect_text(Image={'S3Object': {'Bucket': BUCKET_NAME,
                                                            'Name':CAR_PIC_NAME}})

    text_detection = response['TextDetections']
    texts_detected = list(map(lambda x: x['DetectedText'], text_detection))
    plates_detected = list(filter(lambda x: 12>len(x)>7, texts_detected))
    return plates_detected[0] if len(plates_detected) > 0 else ""


def detect_object_in_pic(image_path):
    upload_image(image_path, PIC_NAME)

    client = boto3.client('rekognition')
    response = client.detect_labels(Image={'S3Object':{'Bucket':BUCKET_NAME,
                                                           'Name':PIC_NAME}},
                                    MaxLabels=10)

    return response


def find_car(objects_in_pic):
    def is_car_label(label):
        return label['Name'] == 'Car'

    labels = objects_in_pic['Labels']
    car_instances = list(filter(is_car_label, labels))[0]['Instances']
    car_bounding_boxes = list(map(lambda instance: instance['BoundingBox'], car_instances))
    biggest_box = reduce(lambda box_1,box_2: box_1 if box_1['Width']*box_1['Height'] >= box_2['Width']*box_2['Height'] else box_2,
                         car_bounding_boxes)

    return biggest_box


def convert_to_pixels(img, car_box):
    height, width, dim = img.shape

    car_box['Width'] = int(car_box['Width'] * width)
    car_box['Height'] = int(car_box['Height'] * height)
    car_box['Left'] = int(car_box['Left'] *width)
    car_box['Top'] = int(car_box['Top'] *height)


def get_car_img(image_path):
    objects_in_pic = detect_object_in_pic(image_path)
    car_box = find_car(objects_in_pic)
    img = cv2.imread(image_path)
    convert_to_pixels(img, car_box)
    car_img = img[car_box['Top']:car_box['Top'] + car_box['Height'], car_box['Left']:car_box['Left'] + car_box['Width']]

    return car_img


def main():
    image_path = args["image"]

    car_img_path = f'car_{image_path[:image_path.find(".")]}.JPG'

    car_img = get_car_img(image_path)
    cv2.imwrite(car_img_path, car_img)
    car_plate = detect_car_plate_number(car_img_path)

    print(car_plate)

if __name__ == "__main__":
    main()