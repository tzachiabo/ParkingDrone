import socket
from Mission import *
from concurrent.futures import ThreadPoolExecutor
from functools import partial


class Comm:
    def __init__(self, comm_configuration):
        self.ip = comm_configuration['ip_of_server']
        self.port = comm_configuration['port']
        self.pic_port = comm_configuration['pic_port']
        self.num_of_workers = comm_configuration['num_of_workers']
        self.drone = None
        self.client_socket = None

    def executor_mission(self, mission):
        mission.execute()
        print(f'send message {mission.encode_result()}')

        self.client_socket.send(f'{mission.encode_result()}%'.encode())

    def decoder(self, message):
        sub_message = message.split(' ')
        index = int(sub_message[1])

        if sub_message[0] == 'getStatus':
            return GetStatus(index, self.drone)
        if sub_message[0] == 'getLocation':
            return GetLocation(index)

        print(sub_message[0])

        assert False

    def start(self, drone):
        self.drone = drone
        executor = ThreadPoolExecutor(max_workers=self.num_of_workers)

        self.client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.client_socket.connect((self.ip, self.port))
        data = ''
        while 1:
            data += self.client_socket.recv(512).decode("utf-8")
            if "%" in data:
                while "%" in data:
                    end_of_message = data.find("%")
                    message = data[:end_of_message]
                    data = data[end_of_message + 1:]

                    print(f'got message {message}')
                    mission = self.decoder(message)
                    executor.submit(partial(self.executor_mission, mission))

