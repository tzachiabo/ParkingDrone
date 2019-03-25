#!python3
import sys
import os
import subprocess
from shutil import copyfile

cwd = os.getcwd()

cwd2 = os.path.abspath(os.path.join(cwd, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))
cwd2 = os.path.abspath(os.path.join(cwd2, os.pardir))

cwd2 = os.path.abspath(os.path.join(cwd2, 'drone_mock'))

p = subprocess.Popen(args=['python', 'BL.py'], cwd=cwd2, shell=True)
p.communicate()