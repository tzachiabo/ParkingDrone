import os
from Drone import Drone
from Comm import Comm
import json
import logging


def init_logger(logger_conf):
    os.remove(logger_conf['path'])
    logging.basicConfig(filename=logger_conf['path'], level=getattr(logging, logger_conf['level']),
                        format='%(asctime)s, %(levelname)s %(message)s')


def main():
    f = open('BaseConfig.JSON')
    configuration = json.load(f)

    init_logger(configuration['logger'])
    drone = Drone(configuration['drone'])
    comm = Comm(configuration['transport'])
    comm.start(drone)


if __name__ == "__main__":
    main()
