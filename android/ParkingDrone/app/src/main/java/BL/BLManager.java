package BL;

public class BLManager {
    DroneSingleton  drone;
    public void init(){
        drone = DroneSingleton.getInstance();
    }
}
