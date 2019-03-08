package BL.Drone.DJIM210;

import java.nio.ByteBuffer;
import java.util.List;

import BL.missions.TakePictureMission;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.Camera;
import dji.sdk.codec.DJICodecManager;
import dji.thirdparty.afinal.core.AsyncTask;

public class CameraManager implements DJICodecManager.YuvDataCallback  {
    List<Camera> cameras;
    Camera mainCamera;
    boolean isInitiated;
    private int count;

    public CameraManager(List<Camera> cameras){
        this.cameras = cameras;
        mainCamera = getCamera();
        isInitiated = false;
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
    }

    public boolean isInitiated() {
        if (!isInitiated){
            Logger.info("camera is not initiated yet");
        }
        return isInitiated;
    }

    @Override
    public void onYuvDataReceived(final ByteBuffer yuvFrame, int dataSize, final int width, final int height) {
        //In this demo, we test the YUV data by saving it into JPG files.
        //DJILog.d(TAG, "onYuvDataReceived " + dataSize);
        if (count++ % 30 == 0 && yuvFrame != null) {
            final byte[] bytes = new byte[dataSize];
            yuvFrame.get(bytes);
            //DJILog.d(TAG, "onYuvDataReceived2 " + dataSize);
            AsyncTask.execute(new Runnable() {
                @Override
                public void run() {
//                    saveYuvDataToJPEG(bytes, width, height);
                }
            });
        }
    }
}
