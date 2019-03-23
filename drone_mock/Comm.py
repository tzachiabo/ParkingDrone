import socket
from Mission import *
from concurrent.futures import ThreadPoolExecutor
from functools import partial
from common import Direction, GimbalMoveType, GimbalPosition
import logging


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
        mission.print_if__if_main_mission(f'{mission.encode_result()}')

        self.client_socket.send(f'{mission.encode_result()}%'.encode())

    def decoder(self, message):
        sub_message = message.split(' ')
        index = int(sub_message[1])
        logging.debug(f'start decode {message}')

        if sub_message[0] == 'getStatus':
            return GetStatus(index, self.drone)
        if sub_message[0] == 'getLocation':
            return GetLocation(index, self.drone)
        if sub_message[0] == 'startLanding':
            return Landing(index, self.drone)
        if sub_message[0] == 'takeOff':
            return TakeOff(index, self.drone)
        if sub_message[0] == 'move':
            direction = Direction[sub_message[2]]
            distance = float(sub_message[3])
            return Move(index, self.drone, direction, distance)
        if sub_message[0] == 'moveGimbal':
            gimbal_position = GimbalPosition[sub_message[2]]
            gimbal_movment_type = GimbalMoveType[sub_message[3]]
            roll = float(sub_message[4])
            pitch = float(sub_message[5])
            yaw = float(sub_message[6])
            return MoveGimbal(index, self.drone, gimbal_position, gimbal_movment_type, roll, pitch, yaw)
        if sub_message[0] == 'goToGPS':
            lat = float(sub_message[2])
            lng = float(sub_message[3])
            alt = float(sub_message[4])
            return GoToGps(index, self.drone, lng, lat, alt)
        if sub_message[0] == 'takePhoto':
            return TakePhoto(index, self.drone)

        logging.fatal(f'failed to decode {message}')
        print(sub_message[0])
        logging.shutdown()
        assert False

    def start(self, drone):
        logging.info('start comm')
        self.drone = drone
        executor_of_status_missions = ThreadPoolExecutor(max_workers=self.num_of_workers)
        executor_of_main_missions = ThreadPoolExecutor(max_workers=1)

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

                    mission = self.decoder(message)

                    if mission.is_main_mission():
                        logging.info(f'new main mission has arrived {message}')
                        print(message)
                        executor_of_main_missions.submit(partial(self.executor_mission, mission))
                    else:
                        logging.debug(f'new status mission has arrived {message}')
                        executor_of_status_missions.submit(partial(self.executor_mission, mission))
