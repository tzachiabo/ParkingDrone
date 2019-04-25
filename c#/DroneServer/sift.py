import numpy as np
import cv2
from matplotlib import pyplot as plt
import sys


def rec(img, point):
    cv2.rectangle(img, (int(point[0])-15, int(point[1])-15),
                  (int(point[0])+15, int(point[1])+15), (255, 0, 0), 30)


def corelate_points(matches, kp1, kp2):
    arr = []
    for m in matches:
        # (point in big , point in little)
        arr.append((kp2[m.trainIdx].pt, kp1[m.queryIdx].pt))
    return arr


def make_buckets(points, f):
    buckets = []
    const = 50
    for init in points:
        arr = []
        for p in points:
            if (abs(init[f][0]-p[f][0]) < const and abs(init[f][1]-p[f][1]) < const):
                arr.append(p)
        buckets.append(arr)
    return buckets


def filter_buckets(buckets):
    winners = []
    for b in buckets:
        co_buckets = make_buckets(b, 1)
        maxcb = co_buckets[0]
        for cb in co_buckets:
            if (len(maxcb) < len(cb)):
                maxcb = cb
        winners.append((b, maxcb))

    const = 0
    wow = winners[0]  # winner of winners kaboom!
    for w in winners:
        if(len(wow[1]) - const*(len(wow[0])-len(wow[1])) < len(w[1]) - const*(len(w[0])-len(w[1]))):
            wow = w
    return wow


def get_mid(bucket, little, big,ratio):
    if ratio<1:
	   ratio=1/ratio
    bx = bucket[0][0][0]
    by = bucket[0][0][1]

    lx = bucket[0][1][0]
    ly = bucket[0][1][1]
    mlx = little.shape[1]/2
    mly = little.shape[0]/2
    
    mbx=-1*((lx-mlx)/ratio-bx)
    mby=-1*((ly-mly)/(2*ratio)-by)
	
    return (int(mbx),int(mby))



img1_path = sys.argv[2]
img2_path = sys.argv[1]

ratio= float(sys.argv[3])

img1 = cv2.imread(img1_path, 0)          # queryImage (little)
img2 = cv2.imread(img2_path, 0)          # trainImage (big)

# Initiate SIFT detector
orb = cv2.ORB_create()

# find the keypoints and descriptors with SIFT
kp1, des1 = orb.detectAndCompute(img1, None)
kp2, des2 = orb.detectAndCompute(img2, None)

# create BFMatcher object
bf = cv2.BFMatcher(cv2.NORM_HAMMING, crossCheck=True)

# Match descriptors.
matches = bf.match(des1, des2)

# Sort them in the order of their distance.
matches = sorted(matches, key=lambda x: x.distance)

#sift

little = cv2.imread(img1_path, 1)
big = cv2.imread(img2_path, 1)


points = corelate_points(matches, kp1, kp2)
wb = filter_buckets(make_buckets(points, 0))
mid_point = get_mid(wb[1], little, big,ratio)
print mid_point
rec(big, mid_point)
#rec(big,wb[1][0][0])
#rec(little,wb[1][0][1])

#plt.imshow(big)
#plt.show()
#plt.imshow(little)
#plt.show()


img_output = '{}_{}'.format(img1_path[0:img1_path.find('.')], img2_path[0:img2_path.find('.')])

img3 = cv2.drawMatches(img1, kp1, img2, kp2, matches, img1)
#plt.imshow(img3),plt.show()
cv2.imwrite('{}_a.jpg'.format(img_output), img3)
cv2.imwrite('{}_b.jpg'.format(img_output), big)



############################################################################################
# best_matches = matches[:10]

# img3 = cv2.drawMatches(img1, kp1, img2, kp2, matches, img1)
# plt.imshow(img3), plt.show()


# little = cv2.imread(img1_path, 1)
# big = cv2.imread(img2_path, 1)


# little_dot = kp1[best_matches[0].queryIdx].pt
# big_dot = kp2[best_matches[0].trainIdx].pt


# rec(little, little_dot)
# rec(big, big_dot)

# # print little.shape[0]  # 0 is length
# # print little.shape[1]  # 1 is width
# # print little_dot[0]  # 0 is the width
# # print little_dot[1]  # 1 is the length

# big_dot2 = kp1[best_matches[2].queryIdx].pt
# little_dot2 = kp2[best_matches[2].trainIdx].pt

# # cv2.rectangle(little, (int(big_dot2[0])-15, int(big_dot2[1])-15),
# #               (int(big_dot2[0])+15, int(big_dot2[1])+15), (255, 0, 0), 30)
# # cv2.rectangle(big, (int(little_dot2[0])-15, int(little_dot2[1])-15),
# #               (int(little_dot2[0])+15, int(little_dot2[1])+15), (255, 0, 0), 30)


# ratio_width = (big_dot[0] - big_dot2[0]) / (little_dot[0]-little_dot2[0])
# ratio_length = (big_dot[1] - big_dot2[1]) / (little_dot[1]-little_dot2[1])

# print ratio_width
# print ratio_length
# dlength = little.shape[0] - little_dot[1]
# dwidth = little.shape[1] - little_dot[0]
# # cv2.rectangle(big, (int(little_dot[0]-15 + dwidth*ratio_width), int(little_dot[1]-15 + dlength*ratio_length)),
# #              (int(little_dot[0]+15+dwidth*ratio_width), int(little_dot[1]+15+dlength*ratio_length)), (255, 0, 0), 30)

# plt.imshow(big)
# plt.show()
# plt.imshow(little)
# plt.show()
