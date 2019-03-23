import math
import geopy

# bearing in radian
# distance is km
def getLocationByBearingAndDistance(latitude, longitude, distance, bearing):
    radius = 6378.1
    lat = math.radians(latitude)
    lon = math.radians(longitude)

    lat = math.asin(math.sin(lat) * math.cos(distance / radius) + math.cos(lat) * math.sin(distance / radius) * math.cos(bearing));
    lon += math.atan2(math.sin(bearing) * math.sin(distance / radius) * math.cos(lat),
                      math.cos(distance / radius) - math.sin(lat) * math.sin(lat));

    lat = lat * 180 / math.pi
    lon = lon * 180 / math.pi
    return geopy.Point(lat, lon)