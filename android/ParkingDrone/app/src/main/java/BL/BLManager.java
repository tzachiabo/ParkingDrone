package BL;

import SharedClasses.Logger;

import android.arch.core.util.Function;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Environment;
import android.util.Log;

import java.io.File;
import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

import BL.missions.TakePictureMission;
import SharedClasses.Assertions;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.error.DJISDKError;
import dji.common.flightcontroller.virtualstick.FlightCoordinateSystem;
import dji.common.flightcontroller.virtualstick.RollPitchControlMode;
import dji.common.flightcontroller.virtualstick.VerticalControlMode;
import dji.common.flightcontroller.virtualstick.YawControlMode;
import dji.common.util.CommonCallbacks;
import dji.sdk.base.BaseComponent;
import dji.sdk.base.BaseProduct;
import dji.sdk.camera.Camera;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class BLManager {
    private static BLManager instance = null;
    SocketManager socket_manager;
    public static File file;


    private BLManager() {
        Logger.debug("initiate BL");
        socket_manager = SocketManager.getInstance();
    }

    public static BLManager getInstance() {
        if (instance == null) {
            instance = new BLManager();
        }
        return instance;
    }

    public static void initFs(Context context){
          file = new File(Environment.getExternalStorageDirectory(), "zahibar");
          file.mkdirs();
//        File mydir = context.getDir("mydir",Context.MODE_PRIVATE);
//        file = new File(mydir,"mydir");
//        Logger.info("main dir" + file.getAbsolutePath());
    }
    public static void initCamera() {
        List<Camera> cameras = DJISDKManager.getInstance().getProduct().getCameras();
        Camera camera = null;
        for (Camera c : cameras) {
            if (c.getDisplayName().equals(Config.MAIN_CAMERA_NAME)) {
                camera = c;
                Logger.info("Using camera " + camera.getDisplayName());
            }
        }
        if (camera != null) {
            TakePictureMission.camera = camera;
            camera.setShootPhotoMode(SettingsDefinitions.ShootPhotoMode.SINGLE, new CommonCallbacks.CompletionCallback() {
                @Override
                public void onResult(DJIError djiError) {
                    if (djiError != null) {
                        Logger.error("Setting Photo mode result " + djiError.toString());
                    } else {
                        Logger.info("Photo mode is Single");
                    }
                }
            });
        } else {
            Assertions.verify(false, "Camera object is null at camera init");
        }
    }

}
