import cv2
import numpy as np
import sys
from matplotlib import pyplot as plt
import functools


base_photo_path = sys.argv[1]
current_photo_path = sys.argv[2]
ratio = float(sys.argv[3])
debug = True if len(sys.argv) == 5 else False

img = cv2.imread(base_photo_path, 0)
img2 = img.copy()
template = cv2.imread(current_photo_path, 0)
height, width = template.shape
template = cv2.resize(template, (int(width*ratio), int(height*ratio)))

w, h = template.shape[::-1]

# All the 6 methods for comparison in a list
methods = ['cv2.TM_CCOEFF', 'cv2.TM_CCOEFF_NORMED',
           'cv2.TM_CCORR_NORMED', 'cv2.TM_SQDIFF', 'cv2.TM_SQDIFF_NORMED']

locations = []

for meth in methods:
    img = img2.copy()
    method = eval(meth)

    # Apply template Matching
    res = cv2.matchTemplate(img, template, method)
    min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)

    # If the method is TM_SQDIFF or TM_SQDIFF_NORMED, take minimum
    if method in [cv2.TM_SQDIFF, cv2.TM_SQDIFF_NORMED]:
        top_left = min_loc
    else:
        top_left = max_loc
    bottom_right = (top_left[0] + w, top_left[1] + h)

    locations.append(((top_left[0]+bottom_right[0])/2,
                      (top_left[1]+bottom_right[1])/2))
					  

def sum(x, y):
    return (x[0]+y[0], x[1]+y[1])


all_sum = functools.reduce(sum, locations)
location = (int(all_sum[0]/len(locations)), int(all_sum[1]/len(locations)))
print(location)
if debug:
    img = cv2.imread(base_photo_path, 1)
    cv2.rectangle(
        img, (location[0]-15, location[1]-15), (location[0]+15, location[1]+15), (255, 0, 0), 30)

    plt.subplot(121), plt.imshow(img)
    plt.title('Matching Result'), plt.xticks([]), plt.yticks([])
    plt.show()
