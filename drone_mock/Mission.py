from abc import ABC, abstractmethod
import logging
import math

class Mission(ABC):
    def __init__(self, index, drone):
        self.index = index
        self.drone = drone

    @abstractmethod
    def execute(self):
        pass

    @abstractmethod
    def encode_result(self):
        pass

    @abstractmethod
    def print_if__if_main_mission(self, msg):
        pass

    @abstractmethod
    def is_main_mission(self):
        pass


class MainMission(Mission):
    def __init__(self, index, drone):
        super(MainMission, self).__init__(index, drone)

    @abstractmethod
    def execute(self):
        pass

    @abstractmethod
    def encode_result(self):
        pass

    def print_if__if_main_mission(self, msg):
        print(msg)

    def is_main_mission(self):
        return True


class StatusMission(Mission):
    def __init__(self, index, drone):
        super(StatusMission, self).__init__(index, drone)

    @abstractmethod
    def execute(self):
        pass

    @abstractmethod
    def encode_result(self):
        pass

    def print_if__if_main_mission(self, msg):
        pass

    def is_main_mission(self):
        return False


class GetStatus(StatusMission):
    def __init__(self, index, drone):
        super(GetStatus, self).__init__(index, drone)

    def execute(self):
        pass

    def encode_result(self):
        return f'getStatus {self.index} Done Connected'


class GetLocation(StatusMission):
    def __init__(self, index, drone):
        super(GetLocation, self).__init__(index, drone)

    def execute(self):
        pass

    def encode_result(self):
        drone_bearing_degree = self.drone.bearing_radians / math.pi * 180
        return f'getLocation {self.index} Done {self.drone.alt} {self.drone.lat} {self.drone.lng} {drone_bearing_degree}'


class TakeOff(MainMission):
    def __init__(self, index, drone):
        super(TakeOff, self).__init__(index, drone)

    def execute(self):
        self.drone.take_off()

    def encode_result(self):
        return f'takeOff {self.index} Done'


class Landing(MainMission):
    def __init__(self, index, drone):
        super(Landing, self).__init__(index, drone)

    def execute(self):
        self.drone.landing()

    def encode_result(self):
        return f'startLanding {self.index} Done'


class Move(MainMission):
    def __init__(self, index, drone, direction, amount):
        super(Move, self).__init__(index, drone)
        self.direction = direction
        self.amount = amount

    def execute(self):
        self.drone.relative_move(self.direction, self.amount)

    def encode_result(self):
        return f'move {self.index} Done'


class MoveGimbal(MainMission):
    def __init__(self, index, drone, gimbal_position, gimbal_movment_type, roll, pitch, yaw):
        super(MoveGimbal, self).__init__(index, drone)
        self.gimbal_position = gimbal_position
        self.gimbal_movment_type = gimbal_movment_type
        self.roll = roll
        self.pitch = pitch
        self.yaw = yaw

    def execute(self):
        self.drone.move_gimbal(self.gimbal_position, self.gimbal_movment_type,
                               self.roll, self.pitch, self.yaw)

    def encode_result(self):
        return f'moveGimbal {self.index} Done'


class GoToGps(MainMission):
    def __init__(self, index, drone, lng, lat, alt):
        super(GoToGps, self).__init__(index, drone)
        self.lng = lng
        self.lat = lat
        self.alt = alt

    def execute(self):
        self.drone.go_to_gps(self.lng, self.lat, self.alt)

    def encode_result(self):
        return f'goToGPS {self.index} Done'


class TakePhoto(MainMission):
    def __init__(self, index, drone):
        super(TakePhoto, self).__init__(index, drone)

    def execute(self):
        self.drone.take_photo()

    def encode_result(self):
        return f'takePhoto {self.index} Done pic.JPG'

