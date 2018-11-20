package BL;

import java.util.List;

import BL.missions.TakePictureMission;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.DroneStatus;
import SharedClasses.Logger;
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

public class DroneInitiator {

    private boolean isCameraInitiated;
    private boolean isFlightControllerInitiated;
    private static DroneInitiator instance;

    public static void init(){
        DroneInitiator drone = getInstance();
        if (!drone.isFlightControllerInitiated)
        {
            drone.initFlightController();
        }
        if (!drone.isCameraInitiated)
        {
            drone.initCamera();
        }
    }

    public static boolean isInitiated(){
        DroneInitiator drone = getInstance();
        return drone.isFlightControllerInitiated && drone.isCameraInitiated;
    }

    private static DroneInitiator getInstance(){
        if (instance == null)
            instance = new DroneInitiator();
        return instance;
    }

    private DroneInitiator(){
        isCameraInitiated = false;
        isFlightControllerInitiated = false;
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
                    isFlightControllerInitiated = true;
                }
            }
        });
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
                    isCameraInitiated = true;
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
