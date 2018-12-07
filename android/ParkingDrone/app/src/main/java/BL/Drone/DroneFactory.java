package BL.Drone;

import BL.Drone.DJIM210.M210Manager;

public class DroneFactory {

    public static IDrone getDroneManager(){
        return M210Manager.getInstance();
    }
}
