package BL;

public class BLManager {
    private static BLManager instance = null;
    DroneSingleton  drone;
    SocketManager socket_manager;

    private BLManager(){
        drone = DroneSingleton.getInstance();
        socket_manager = SocketManager.getInstance();
    }
    public static BLManager getInstance(){
        if(instance == null){
            instance = new BLManager();
        }
        return instance;
    }
}
