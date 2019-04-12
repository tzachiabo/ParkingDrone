from enum import Enum
import logging
import math


class CameraType(Enum):
    SimpleCamera = 0
    AerialViewCamera = 1


class Direction(Enum):
    forward = 0
    backward = 1
    right = 2
    left = 3
    up = 4
    down = 5
    rtt_right = 6
    rtt_left = 7


class GimbalMoveType(Enum):
    absolute = 0,
    relative = 1


class GimbalPosition(Enum):
    left = 0,
    right = 1


def verify(predicate, msg):
    if not predicate:
        logging.fatal(msg)
        print(msg)
        logging.shutdown()
        assert False, msg


def rad_to_deg(rad):
    return rad / math.pi * 180
