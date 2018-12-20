#!python3
import sys
import os
import subprocess
from shutil import copyfile

cwd = os.getcwd()
base_photo_name = sys.argv[1]

cwd2 = os.path.abspath(os.path.join(cwd, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))

cwd2 = os.path.abspath(os.path.join(cwd2, 'car_detector\\build\\darknet\\x64'))

copyfile(cwd + '\\' + base_photo_name , cwd2 + '\\' + os.path.basename(base_photo_name))


p = subprocess.Popen(args=['darknet_no_gpu.exe','detect','yolov3-aerial.cfg','yolov3-aerial.weights','cfg/coco.data', os.path.basename(base_photo_name)], cwd=cwd2, shell=True)
p.communicate()