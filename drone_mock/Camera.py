from abc import ABC, abstractmethod
from common import CameraType, Direction, verify
import os.path
import socket
import cv2
import time
import math
import geopy.distance
import logging


def to_rad(degree):
    return degree * math.pi / 180


def send_image(ip, port, buff):
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    client_socket.connect((ip, port))
    client_socket.send(buff)


class Camera(ABC):
    def __init__(self, drone, conf):
        if conf['connected_gimbal'] == 'left':
            self.gimbal = drone.left_gimbal
        elif conf['connected_gimbal'] == 'right':
            self.gimbal = drone.right_gimbal
        else:
            assert False

        self.server_ip = conf['ip_server']
        self.server_port = int(conf['port'])
        self.camera_opening_degree = int(conf['camera_opening_degree'])

    @abstractmethod
    def take_photo(self, drone_height):
        pass


class SimpleCamera(Camera):
    def __init__(self, drone, conf):
        super(SimpleCamera, self).__init__(drone, conf)
        self.images_path = conf['images_path']
        self.image_index = 1

    def take_photo(self, drone_height):
        logging.info('start take photo at Camera')
        file_path = f'{self.images_path}/{str(self.image_index)}.JPG'
        verify(os.path.isfile(file_path), f'pic not exist {file_path}')

        with open(file_path, 'rb') as image_file:
            logging.info('start read pic')
            image_buffer = image_file.read()
            logging.info('start send pic')
            send_image(self.server_ip, self.server_port, image_buffer)

        logging.info('finish take photo at Camera')
        self.image_index += 1


class AerialViewCamera(Camera):
    def __init__(self, drone, conf):
        super(AerialViewCamera, self).__init__(drone, conf)
        self.images_path = conf['images_path']
        self.base_photo_location = f'{self.images_path}/{conf["base_photo_location"]}'

        self.weight_of_width_pixel_per_meter = float(conf['weight_of_width_pixel_per_meter'])
        self.weight_of_height_pixel_per_meter = float(conf['weight_of_height_pixel_per_meter'])

        verify(os.path.isfile(self.base_photo_location), f'pic not exist {self.base_photo_location}')

        self.num_of_width_pixels = None
        self.num_of_height_pixels = None
        self.base_photo_height = None
        self.base_photo_bearing = None

        self.lat_of_base_photo = None
        self.lng_of_base_photo = None

    def take_photo(self, drone):
        verify(self.gimbal.roll == 0 and self.gimbal.pitch == -90 and self.gimbal.yaw == 0,
               'gimbal is not set to aerial position')

        if not self.base_photo_height:
            self.send_base_photo(drone)
        else:
            self.generate_and_send_photo(drone)

    def send_base_photo(self, drone):
        verify(self.num_of_width_pixels is None and self.num_of_height_pixels is None and
               self.base_photo_height is None and self.base_photo_bearing is None, '')

        logging.info('start send_base_photo')
        img = cv2.imread(self.base_photo_location, 0)
        self.num_of_height_pixels, self.num_of_width_pixels = img.shape

        self.base_photo_height = drone.alt
        self.lat_of_base_photo = drone.lat
        self.lng_of_base_photo = drone.lng
        self.base_photo_bearing = drone.bearing_radians

        with open(self.base_photo_location, 'rb') as image_file:
            logging.info('start read base photo for sending')
            image_buffer = image_file.read()
            logging.info('start send base photo')
            send_image(self.server_ip, self.server_port, image_buffer)

        logging.info('finish send_base_photo')

    def get_pixels_size_of_base_photo(self, drone_height):

        ratio = drone_height / self.base_photo_height

        num_of_height_pixel_in_base_photo = int(ratio * self.num_of_height_pixels)
        num_of_width_pixel_in_base_photo = int(ratio * self.num_of_width_pixels)

        return num_of_height_pixel_in_base_photo, num_of_width_pixel_in_base_photo

    def get_margins(self, lat, lng):
        def measure(lat_des, lng_des):
            coords_1 = (self.lat_of_base_photo, self.lng_of_base_photo)
            coords_2 = (lat_des, lng_des)

            return geopy.distance.geodesic(coords_1, coords_2).m

        width_dif_from_base_photo = measure(self.lat_of_base_photo, lng)
        if lng < self.lng_of_base_photo:
            width_dif_from_base_photo = width_dif_from_base_photo * -1

        height_dif_from_base_photo = measure(lat, self.lng_of_base_photo)
        if lat > self.lat_of_base_photo:
            height_dif_from_base_photo = height_dif_from_base_photo * -1

        width_size = self.base_photo_height * self.weight_of_width_pixel_per_meter
        height_size = self.base_photo_height * self.weight_of_height_pixel_per_meter

        base_photo_pixel_margin_left = int(self.num_of_width_pixels / 2)
        base_photo_pixel_margin_top = int(self.num_of_height_pixels / 2)

        pixel_margin_left = base_photo_pixel_margin_left + int((width_dif_from_base_photo / width_size) * self.num_of_width_pixels)
        pixel_margin_top = base_photo_pixel_margin_top + int((height_dif_from_base_photo / height_size) * self.num_of_height_pixels)

        verify(pixel_margin_left < self.num_of_width_pixels, 'margin left is out of base_photo')
        verify(pixel_margin_top < self.num_of_height_pixels, 'margin top is out of base_photo')

        return pixel_margin_top, pixel_margin_left

    def generate_and_send_photo(self, drone):
        # verify(self.base_photo_bearing == 0, 'bearing is not align with photo currently not support rotating')
        logging.info('start generate_and_send_photo')
        logging.info('start generate_and_send_photo')

        height_pixel_in_base_photo, width_pixel_in_base_photo = self.get_pixels_size_of_base_photo(drone.alt)
        img = cv2.imread(self.base_photo_location, 0)

        width_radius = int(width_pixel_in_base_photo / 2)
        height_radius = int(height_pixel_in_base_photo / 2)

        pixel_margin_top, pixel_margin_left = self.get_margins(drone.lat, drone.lng)

        cropped_img = img[pixel_margin_top - height_radius:pixel_margin_top + height_radius,
                          pixel_margin_left - width_radius:pixel_margin_left + width_radius]

        resized_img = cv2.resize(cropped_img, (self.num_of_width_pixels, self.num_of_height_pixels))
        time_stamp = int(time.time())
        image_path = f'./{self.images_path}/generated_pic/{str(time_stamp)}.JPG'
        cv2.imwrite(image_path, resized_img)

        with open(image_path, 'rb') as image_file:
            image_buffer = image_file.read()
            send_image(self.server_ip, self.server_port, image_buffer)

        logging.info('finish generate_and_send_photo')


def camera_factory(drone, conf):
    camera_type = CameraType[conf['camera_type']]
    if camera_type == CameraType.SimpleCamera:
        logging.info('using SimpleCamera')
        return SimpleCamera(drone, conf)
    elif camera_type == CameraType.AerialViewCamera:
        logging.info('using AerialViewCamera')
        return AerialViewCamera(drone, conf)
    else:
        verify(False, 'Unknown camera type')
