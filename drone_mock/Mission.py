from abc import ABC, abstractmethod


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


class GetStatus(Mission):
    def __init__(self, index, drone):
        super(GetStatus, self).__init__(index, drone)

    def execute(self):
        pass

    def encode_result(self):
        return f'getStatus {self.index} Done Connected'


class GetLocation(Mission):
    def __init__(self, index, drone):
        super(GetLocation, self).__init__(index, drone)

    def execute(self):
        pass

    def encode_result(self):
        return f'getLocation {self.index} Done {self.drone.alt} {self.drone.lat} {self.drone.lng}'


