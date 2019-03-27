import os
from Drone import Drone
from Comm import Comm
import json
import logging
import argparse


parser = argparse.ArgumentParser(description='Run Drone Simulator')
parser.add_argument('--override_params', nargs='+', default=[],
                    help='params to override of base config')


def init_base_config():
    f = open('BaseConfig.JSON')
    configuration = json.load(f)

    args = vars(parser.parse_args())
    override_params = args['override_params']

    for params_to_override in override_params:
        key, value = params_to_override.split('=')

        paths = key.split('.')
        last_path = paths.pop()

        tmp_conf = configuration
        for path in paths:
            tmp_conf = tmp_conf[path]
        tmp_conf[last_path] = value

    return configuration


def init_logger(logger_conf):
    os.remove(logger_conf['path'])
    logging.basicConfig(filename=logger_conf['path'], level=getattr(logging, logger_conf['level']),
                        format='%(asctime)s, %(levelname)s %(message)s')


def main():
    configuration = init_base_config()

    init_logger(configuration['logger'])
    drone = Drone(configuration['drone'])
    comm = Comm(configuration['transport'])
    comm.start(drone)


if __name__ == "__main__":
    main()
