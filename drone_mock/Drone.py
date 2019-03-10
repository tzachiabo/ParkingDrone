

class Drone:
    def __init__(self, drone_configuration):
        self.alt = 0
        self.lng = drone_configuration['initial_lng']
        self.lat = drone_configuration['initial_lat']
        self.mistake_in_move = drone_configuration['mistake_in_move']

