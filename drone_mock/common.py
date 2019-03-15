from enum import Enum


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
    rotate = 6


class GimbalMoveType(Enum):
    absolute = 0,
    relative = 1


class GimbalPosition(Enum):
    left = 0,
    right = 1


def verify(predicate, msg):
    if not predicate:
        print(msg)
        assert False, msg

