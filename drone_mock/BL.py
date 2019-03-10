
from Drone import Drone
from Comm import Comm
import json


def main():
    f = open('BaseConfig.JSON')
    configuration = json.load(f)
    drone = Drone(configuration['drone'])
    comm = Comm(configuration['transport'])
    comm.start(drone)


if __name__ == "__main__":
    main()
