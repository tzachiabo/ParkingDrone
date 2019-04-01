#!python3
import sys
import os
import subprocess
from shutil import copyfile

cwd = os.getcwd()
base_photo_name = sys.argv[1]


while os.path.basename(cwd) != 'ParkingDrone':
    cwd = os.path.abspath(os.path.join(cwd, os.pardir))

cwd = os.path.abspath(os.path.join(cwd, 'car_number_plate_recognition'))

copyfile(os.path.abspath(os.path.join(os.getcwd(), base_photo_name)), 
         os.path.abspath(os.path.join(cwd, os.path.basename(base_photo_name))))


p = subprocess.Popen(args=['alpr.exe', os.path.basename(base_photo_name),'-c','il'], cwd=cwd, shell=True)
p.communicate()