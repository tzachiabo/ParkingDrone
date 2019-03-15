from abc import ABC, abstractmethod
from common import CameraType, Direction, verify
import os.path
import socket
import cv2
import time
import math


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
        self.server_port = conf['port']
        self.camera_opening_degree = conf['camera_opening_degree']

    @abstractmethod
    def take_photo(self, drone_height):
        pass

    @abstractmethod
    def move(self, direction, amount):
        pass


class SimpleCamera(Camera):
    def __init__(self, drone, conf):
        super(SimpleCamera, self).__init__(drone, conf)
        self.images_path = conf['images_path']
        self.image_index = 1

    def take_photo(self, drone_height):
        file_path = f'{self.images_path}/{str(self.image_index)}.JPG'
        verify(os.path.isfile(file_path), f'pic not exist {file_path}')

        with open(file_path, 'rb') as image_file:
            image_buffer = image_file.read()
            send_image(self.server_ip, self.server_port, image_buffer)

        self.image_index += 1

    def move(self, direction, amount):
        pass


class AerialViewCamera(Camera):
    def __init__(self, drone, conf):
        super(AerialViewCamera, self).__init__(drone, conf)
        self.images_path = conf['images_path']
        self.base_photo_location = f'{self.images_path}/1.JPG'
        verify(os.path.isfile(self.base_photo_location), f'pic not exist {self.base_photo_location}')

        self.num_of_width_pixels = None
        self.num_of_height_pixels = None
        self.pixel_margin_left = None
        self.pixel_margin_top = None
        self.base_photo_height = None
        self.base_photo_bearing = None

    def take_photo(self, drone_height):
        verify(self.gimbal.roll == 0 and self.gimbal.pitch == -90 and self.gimbal.yaw == 0,
               'gimbal is not set to aerial position')

        if not self.base_photo_height:
            self.send_base_photo(drone_height)
        else:
            self.generate_and_send_photo(drone_height)

    def move(self, direction, amount):
        if direction == Direction.up:
            pass
        elif direction == Direction.down:
            pass
        elif direction == Direction.forward:
            self.move_pixels(0, -1 * amount)
        elif direction == Direction.backward:
            self.move_pixels(0, amount)
        elif direction == Direction.right:
            self.move_pixels(amount, 0)
        elif direction == Direction.left:
            self.move_pixels(-1 * amount, 0)
        else:
            verify(False, "Unknown direction")

    def move_pixels(self, width_diff_in_meter, height_diff_in_meter):
        size = 2 * self.base_photo_height * math.tan(to_rad(self.camera_opening_degree))

        self.pixel_margin_left = self.pixel_margin_left + int((width_diff_in_meter / size) * self.num_of_width_pixels)
        self.pixel_margin_top = self.pixel_margin_top + int((height_diff_in_meter / size) * self.num_of_height_pixels)

        verify(self.pixel_margin_left < self.num_of_width_pixels, 'margin left is out of base_photo')
        verify(self.pixel_margin_top < self.num_of_height_pixels, 'margin top is out of base_photo')

    def send_base_photo(self, drone_height):
        assert self.num_of_width_pixels is None and self.num_of_height_pixels is None and \
               self.pixel_margin_left is None and self.pixel_margin_top is None and \
               self.base_photo_height is None and self.base_photo_bearing is None

        img = cv2.imread(self.base_photo_location, 0)
        self.num_of_height_pixels, self.num_of_width_pixels = img.shape
        self.pixel_margin_left = int(self.num_of_width_pixels / 2)
        self.pixel_margin_top = int(self.num_of_height_pixels / 2)
        self.base_photo_height = drone_height
        self.base_photo_bearing = 0

        with open(self.base_photo_location, 'rb') as image_file:
            image_buffer = image_file.read()
            send_image(self.server_ip, self.server_port, image_buffer)

    def get_pixels_size_of_base_photo(self, drone_height):
        ratio = drone_height / self.base_photo_height

        num_of_height_pixel_in_base_photo = int(ratio * self.num_of_height_pixels)
        num_of_width_pixel_in_base_photo = int(ratio * self.num_of_width_pixels)

        return num_of_height_pixel_in_base_photo, num_of_width_pixel_in_base_photo

    def generate_and_send_photo(self, drone_height):
        verify(self.base_photo_bearing == 0, 'bearing is not align with photo currently not support rotating')

        height_pixel_in_base_photo, width_pixel_in_base_photo = self.get_pixels_size_of_base_photo(drone_height)
        img = cv2.imread(self.base_photo_location, 0)

        width_radius = int(width_pixel_in_base_photo / 2)
        height_radius = int(height_pixel_in_base_photo / 2)

        cropped_img = img[self.pixel_margin_top - height_radius:self.pixel_margin_top + height_radius,
                          self.pixel_margin_left - width_radius:self.pixel_margin_left + width_radius]

        resized_img = cv2.resize(cropped_img, (self.num_of_width_pixels, self.num_of_height_pixels))
        time_stamp = int(time.time())
        image_path = f'./{self.images_path}/generated_pic/{str(time_stamp)}.JPG'
        cv2.imwrite(image_path, resized_img)

        with open(image_path, 'rb') as image_file:
            image_buffer = image_file.read()
            send_image(self.server_ip, self.server_port, image_buffer)


def camera_factory(drone, conf):
    camera_type = CameraType[conf['camera_type']]
    if camera_type == CameraType.SimpleCamera:
        return SimpleCamera(drone, conf)
    elif camera_type == CameraType.AerialViewCamera:
        return AerialViewCamera(drone, conf)
    else:
        verify(False, 'Unknown camera type')
