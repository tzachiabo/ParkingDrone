from abc import ABC, abstractmethod
import os.path
import socket


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

    @abstractmethod
    def take_photo(self):
        pass


class SimpleCamera(Camera):
    def __init__(self, drone, conf):
        super(SimpleCamera, self).__init__(drone, conf)
        self.images_path = conf['images_path']
        self.image_index = 1

    def take_photo(self):
        file_path = f'{self.images_path}/{str(self.image_index)}.JPG'
        assert os.path.isfile(file_path), f'pic not exist {file_path}'

        with open(file_path, 'rb') as image_file:
            image_buffer = image_file.read()
            send_image(self.server_ip, self.server_port, image_buffer)

        self.image_index += 1

