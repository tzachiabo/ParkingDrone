package BL.Drone.DJIM210;

import java.util.List;

import BL.missions.TakePictureMission;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.Camera;

public class CameraManager {
    List<Camera> cameras;
    Camera mainCamera;
    boolean isInitiated;

    public CameraManager(List<Camera> cameras){
        this.cameras = cameras;
        mainCamera = getCamera();
        isInitiated = false;
        initCamera();
    }

    private Camera getCamera() {
        for (Camera c : cameras) {
            if (c.getDisplayName().equals(Config.MAIN_CAMERA_NAME)) {
                return c;
            }
        }

        Assertions.verify(false, "camera " + Config.MAIN_CAMERA_NAME + "could not be found");
        return null;
    }

    private void initCamera() {
        TakePictureMission.camera = mainCamera;
        mainCamera.setShootPhotoMode(SettingsDefinitions.ShootPhotoMode.SINGLE, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null) {
                    Logger.error("Setting Photo mode resulted " + djiError.toString());
                    Assertions.verify(false, "failed to set camera mode to single");
                } else {
                    Logger.info("Photo mode is Single");
                    isInitiated = true;
                }
            }
        });
    }

    public boolean isInitiated() {
        return isInitiated;
    }
}
