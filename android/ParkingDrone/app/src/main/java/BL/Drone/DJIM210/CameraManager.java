package BL.Drone.DJIM210;

import java.lang.reflect.Array;
import java.util.List;

import BL.missions.TakePictureMission;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.Camera;
import dji.sdk.camera.VideoFeeder;

public class CameraManager {
    List<Camera> cameras;
    Camera mainCamera;
    Object lock_for_image;
    byte[] img;
    boolean isInitiated;

    public CameraManager(List<Camera> cameras){
        this.cameras = cameras;
        mainCamera = getCamera();
        isInitiated = false;
        img = null;
        lock_for_image = new Object();
        initCamera();
    }

    private Camera getCamera() {
        Assertions.verify(cameras != null, "camera " + Config.MAIN_CAMERA_NAME + " could not be found");

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
                    try {
                        Thread.sleep(2000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    initCamera();
                } else {
                    Logger.info("Photo mode is Single");
                    isInitiated = true;
                }
            }
        });
        VideoFeeder.VideoFeed video_feed = VideoFeeder.getInstance().getPrimaryVideoFeed();
        video_feed.addVideoDataListener(new VideoFeeder.VideoDataListener(){
            @Override
            public void onReceive(byte[] bytes, int size) {
                img = bytes.clone();
            }
        });
    }

    public boolean isInitiated() {
        if (!isInitiated){
            Logger.info("camera is not initiated yet");
        }
        return isInitiated;
    }

    public byte[] getImg()
    {
        Assertions.verify(img != null, "img is null");
        return img;
    }
}
