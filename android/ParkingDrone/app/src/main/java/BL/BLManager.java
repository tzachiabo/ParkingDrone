package BL;

import SharedClasses.Config;
import SharedClasses.DroneStatus;
import SharedClasses.Logger;
import android.os.Environment;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;
import BL.missions.TakePictureMission;
import SharedClasses.Assertions;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.flightcontroller.virtualstick.FlightCoordinateSystem;
import dji.common.flightcontroller.virtualstick.RollPitchControlMode;
import dji.common.flightcontroller.virtualstick.VerticalControlMode;
import dji.common.flightcontroller.virtualstick.YawControlMode;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.Camera;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class BLManager {
    private static BLManager instance = null;
    private SocketManager socket_manager;
    public File file;
    private AtomicInteger DroneStatus;
    private boolean isConnected;

    private BLManager() {
        Logger.debug("initiate BL");
        isConnected = false;
        socket_manager = SocketManager.getInstance();
        DroneStatus = new AtomicInteger();
    }

    public static BLManager getInstance() {
        if (instance == null) {
            instance = new BLManager();
        }
        return instance;
    }

    public synchronized void init(){
        initFs();
        initCamera();
        initFlightController();
        isConnected = true;
    }

    public synchronized void DisconnectDrone(){
        isConnected = false;
    }

    public synchronized SharedClasses.DroneStatus getDroneStatus(){
        if (isDroneReady() && isConnected)
            return SharedClasses.DroneStatus.Ready;
        else if (isConnected)
            return SharedClasses.DroneStatus.NotReady;
        else
            return SharedClasses.DroneStatus.Disconnected;
    }

    private void initFlightController(){
        Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "while init flight controller aircraft was null");

        aircraft.getFlightController().setRollPitchControlMode(RollPitchControlMode.VELOCITY);
        aircraft.getFlightController().setYawControlMode(YawControlMode.ANGULAR_VELOCITY);
        aircraft.getFlightController().setVerticalControlMode(VerticalControlMode.VELOCITY);
        aircraft.getFlightController().setRollPitchCoordinateSystem(FlightCoordinateSystem.BODY);

        aircraft.getFlightController().setVirtualStickModeEnabled(true, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null) {
                    Logger.error("Setting virtual stick mode resulted " + djiError.toString());
                    Assertions.verify(false, "failed to set virtual stick");
                } else {
                    Logger.info("init FlightController has been done");
                    DroneStatus.getAndIncrement();
                }
            }
        });
    }

    public boolean isDroneReady(){
        return DroneStatus.get() == 3;
    }

    private void initFs(){
          file = new File(Environment.getExternalStorageDirectory(),
                          Config.DJI_PHOTO_DIR);
          file.mkdirs();
          Logger.info("main dir" + file.getAbsolutePath());
          DroneStatus.getAndIncrement();
    }

    private void initCamera() {
        Camera camera = getCamera();
        TakePictureMission.camera = camera;
        camera.setShootPhotoMode(SettingsDefinitions.ShootPhotoMode.SINGLE, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null) {
                    Logger.error("Setting Photo mode resulted " + djiError.toString());
                    Assertions.verify(false, "failed to set camera mode to single");
                } else {
                    Logger.info("Photo mode is Single");
                    DroneStatus.getAndIncrement();
                }
            }
        });
    }

    private Camera getCamera(){
        Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "while init camera aircraft was null");
        List<Camera> cameras = DJISDKManager.getInstance().getProduct().getCameras();

        for (Camera c : cameras) {
            if (c.getDisplayName().equals(Config.MAIN_CAMERA_NAME)) {
                return c;
            }
        }

        Assertions.verify(false, "camera "+ Config.MAIN_CAMERA_NAME +"could not be found");
        return null;
    }

}
