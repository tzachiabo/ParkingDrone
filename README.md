# ParkingDrone
Developing an automate system that will be able to identify cars that park illegally. And produce a parking ticket for those cars.

**Arcituture -** 
![alt text](https://raw.githubusercontent.com/tzachiabo/ParkingDrone/master/arcituture.PNG)

**Mediator –**
Android OS based machine connected to the controller and mediates between the Drone and the Mission Manager server.

**Mission manager** is written in c#
Server which uses computer vision modules to analyze real-time photos and manage the drone accordingly.

**Car detector** is using YOLOV3 for detecting cars from aerial view 

**Car plate detector** is using AWS rekognition
