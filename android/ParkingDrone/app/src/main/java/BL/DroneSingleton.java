package BL;

public class DroneSingleton {

    private static DroneSingleton drone;
    private DroneSingleton(){

    }
    public static DroneSingleton getInstance(){
        if(drone==null)
            drone = new DroneSingleton();
        return drone;
    }
}
