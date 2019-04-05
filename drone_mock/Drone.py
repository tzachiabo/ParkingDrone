import random
from common import Direction, GimbalMoveType, GimbalPosition, verify
import Camera
import math
import time
import numpy
import geopy.distance
from GeoHelper import getLocationByBearingAndDistance
import logging


def _align_bearing(bearing):
    logging.info(f'_align_bearing: bearing before alignment: {bearing}')

    if bearing < 0:
        bearing += 2 * math.pi

    if bearing > 2 * math.pi:
        bearing -= 2 * math.pi

    logging.info(f'_align_bearing: bearing after alignment: {bearing}')

    return bearing


def _get_range(start, end, num_of_points):
    if start != end:
        delta = end - start
        return numpy.arange(start, end, delta / num_of_points)
    else:
        return [start for _ in range(num_of_points)]


class Drone:
    # not support delay and stop
    # not support rotate the drone
    def __init__(self, drone_configuration):
        logging.info('start drone')
        self.alt = 0
        self.lng = float(drone_configuration['initial_lng'])
        self.lat = float(drone_configuration['initial_lat'])
        self.bearing_radians = float(drone_configuration['initial_bearing_radians'])
        self.speed_in_ms = float(drone_configuration['speed_in_ms'])

        self.mistake_in_move = float(drone_configuration['mistake_in_move'])

        self.min_take_off_height = float(drone_configuration['take_off']['min_height'])
        self.max_take_off_height = float(drone_configuration['take_off']['max_height'])

        self.gimbal_delay_in_sec = float(drone_configuration['gimbal_delay_in_sec'])

        self.left_gimbal = Gimbal()
        self.right_gimbal = Gimbal()

        self.camera = Camera.camera_factory(self, drone_configuration['camera'])

        self.move_step_size = float(drone_configuration['move_step_size'])

        self.without_delay = drone_configuration['without_delay'] == 'true'

    def take_off(self):
        verify(self.alt == 0, "drone height should have been 0")
        self.alt = random.uniform(self.min_take_off_height, self.max_take_off_height)
        logging.info(f'drone was take off and now at {self.alt} height')

    def landing(self):
        verify(self.alt != 0, "drone height should have been 0")
        self.alt = 0
        logging.info(f'drone was landed')

    def _get_points_in_the_middle(self, destination, distance):
        amount_of_points = abs(int(distance / self.move_step_size))

        lat_points = _get_range(self.lat, destination.latitude, amount_of_points)
        lng_points = _get_range(self.lng, destination.longitude, amount_of_points)

        if len(lat_points) > len(lng_points):
            lat_points = lat_points[:len(lng_points)]

        if len(lng_points) > len(lat_points):
            lng_points = lng_points[:len(lat_points)]

        return [geopy.Point(lat, lng) for lat, lng in zip(lat_points, lng_points)]

    def relative_move_inner(self, distance, bearing):
        bearing = _align_bearing(bearing)
        destination = getLocationByBearingAndDistance(self.lat, self.lng, distance/1000, bearing)

        logging.info(f'start relative_move_inner drone-bearing: {self.bearing_radians} '
                     f'bearing direction {bearing} '
                     f'distance: {distance} '
                     f'from: lat: {self.lat}, lng: {self.lng} '
                     f'to: lat: {destination.latitude}, lng {destination.longitude}')

        middle_points = self._get_points_in_the_middle(destination, distance)
        logging.info(f'middle point collcted')

        for point in middle_points:
            self.lat = point.latitude
            self.lng = point.longitude
            self.sleep(self.move_step_size / self.speed_in_ms)

        logging.info(f'finish relative_move_inner drone distance: {distance}, bearing: {bearing}')

    def rotate_drone(self, rotataion_degree):
        if rotataion_degree > 360:
            rotataion_degree -= 360

        self.bearing_radians += rotataion_degree / 180 * math.pi

        if self.bearing_radians > math.pi:
            self.bearing_radians -= 2 * math.pi

    def relative_move(self, direction, amount):
        logging.info(f'start drone move with direction: {direction} and amount: {amount}')
        error = amount * self.mistake_in_move
        actual_move_amount = random.uniform(amount - error, amount + error)
        logging.info(f'drone actual move amount is {actual_move_amount}')

        if direction == Direction.up:
            self.alt += actual_move_amount
        elif direction == Direction.down:
            self.alt -= actual_move_amount
        elif direction == Direction.forward:
            self.relative_move_inner(actual_move_amount, self.bearing_radians)
        elif direction == Direction.backward:
            self.relative_move_inner(-1 * actual_move_amount, self.bearing_radians)
        elif direction == Direction.right:
            self.relative_move_inner(actual_move_amount, self.bearing_radians + math.pi / 2)
        elif direction == Direction.left:
            self.relative_move_inner(actual_move_amount, self.bearing_radians + 3 * math.pi / 2)
        elif direction == Direction.rotate:
            verify(amount < 360, "drone cant rotate more than 360 degrees")
            self.rotate_drone(actual_move_amount)
        else:
            verify(False, "Unknown direction")

        logging.info(f'end drone move with direction: {direction} and amount: {amount}')

    def move_gimbal(self, gimbal_position, gimbal_movment_type, roll, pitch, yaw):
        logging.info('start move gimbal')
        time.sleep(self.gimbal_delay_in_sec)
        if gimbal_position == GimbalPosition.left:
            if gimbal_movment_type == GimbalMoveType.absolute:
                self.left_gimbal.move_absolute(roll, pitch, yaw)
            else:
                self.left_gimbal.move_relative(roll, pitch, yaw)
        else:
            if gimbal_movment_type == GimbalMoveType.absolute:
                self.right_gimbal.move_absolute(roll, pitch, yaw)
            else:
                self.right_gimbal.move_relative(roll, pitch, yaw)
        logging.info('finish move gimbal')

    def measure(self, lat_des, lng_des):
        coords_1 = (self.lat, self.lng)
        coords_2 = (lat_des, lng_des)

        return geopy.distance.geodesic(coords_1, coords_2).m

    def _go_to_gps_inner(self, distance_in_meters, start_point, end_point, update_func):
        if distance_in_meters != 0:
            num_of_steps = distance_in_meters / self.move_step_size
            move_amount_per_step = (end_point - start_point) / num_of_steps

            for current_point in numpy.arange(start_point, end_point, move_amount_per_step):
                self.sleep(self.move_step_size / self.speed_in_ms)

                update_func(current_point)

    def go_to_gps(self, lng, lat, alt):
        logging.info('start go to gps')

        if alt != self.alt:
            distance_in_meters = alt - self.alt

            def update_lat(_alt):
                self.alt = _alt

            self._go_to_gps_inner(distance_in_meters, self.alt, alt, update_lat)

        if lng != self.lng:
            distance_in_meters = self.measure(self.lat, lng)

            def update_lng(_lng):
                self.lng = _lng

            self._go_to_gps_inner(distance_in_meters, self.lng, lng, update_lng)

        if lat != self.lat:
            distance_in_meters = self.measure(lat, self.lng)

            def update_lat(_lat):
                self.lat = _lat

            self._go_to_gps_inner(distance_in_meters, self.lat, lat, update_lat)

        self.bearing_radians = random.uniform(-1 * math.pi, math.pi)

    def take_photo(self):
        self.camera.take_photo(self)

    def sleep(self, amount):
        if self.without_delay:
            time.sleep(amount)

class Gimbal:
    def __init__(self):
        self.roll = 0
        self.pitch = 0
        self.yaw = 0

    def move_relative(self, rol, pitch, yaw):
        self.roll += rol
        self.pitch += pitch
        self.yaw += yaw

    def move_absolute(self, rol, pitch, yaw):
        self.roll = rol
        self.pitch = pitch
        self.yaw = yaw
