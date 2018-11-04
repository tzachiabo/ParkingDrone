package BL;

import SharedClasses.Logger;

public class BLManager {
    private static BLManager instance = null;
    SocketManager socket_manager;


    private BLManager()
    {
        Logger.debug("initiate BL");
        socket_manager = SocketManager.getInstance();
    }

    public static BLManager getInstance(){
        if (instance == null)
        {
            instance = new BLManager();
        }
        return instance;
    }



}
