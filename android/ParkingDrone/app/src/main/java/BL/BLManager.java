package BL;

import BL.Drone.DroneInitiator;
import SharedClasses.Config;
import SharedClasses.Logger;

import android.os.Environment;

import java.io.File;

public class BLManager {
    private static BLManager instance = null;
    private SocketManager socket_manager;
    public File file;
    private boolean isFsInitiated;
    private boolean isConnected;

    private BLManager() {
        Logger.debug("initiate BL");
        isConnected = false;
        socket_manager = SocketManager.getInstance();
        isFsInitiated = false;
    }

    public static BLManager getInstance() {
        if (instance == null) {
            instance = new BLManager();
        }
        return instance;
    }

    public synchronized void init() {
        DroneInitiator.init();
        initFs();
        isConnected = true;
    }

    public synchronized void DisconnectDrone() {
        isConnected = false;
    }

    public synchronized SharedClasses.DroneStatus getDroneStatus(){
        if (DroneInitiator.isInitiated() && isFsInitiated && isConnected)
            return SharedClasses.DroneStatus.Connected;
        else if (isConnected)
            return SharedClasses.DroneStatus.NotReady;
        else
            return SharedClasses.DroneStatus.Disconnected;
    }

    private void initFs()
    {
        if (!isFsInitiated){
            file = new File(Environment.getExternalStorageDirectory(),
                    Config.DJI_PHOTO_DIR);
            file.mkdirs();
            Logger.info("main dir" + file.getAbsolutePath());
            isFsInitiated= true;
        }
    }
}
