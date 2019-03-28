#!python3
import sys
import os
import subprocess
from shutil import copyfile

cwd = os.getcwd()

while (os.path.basename(cwd) != "ParkingDrone"):	
	cwd = os.path.abspath(os.path.join(cwd, os.pardir))

cwd = os.path.abspath(os.path.join(cwd, 'drone_mock'))

args = ['python', 'BL.py']
if (len(sys.argv) > 1):
	args.append('--override_params')
	for i in range(len(sys.argv) - 1):
		args.append(sys.argv[i+1])

p = subprocess.Popen(args=args, cwd=cwd, shell=True)
p.communicate()