package BL.missions;

import android.os.Handler;

import SharedClasses.Logger;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.*;
import dji.sdk.sdkmanager.DJISDKManager;


public class TakePictureMission extends Mission {
    final Camera camera= DJISDKManager.getInstance().getProduct().getCamera();
    private Handler handler;
    int size;
    byte[] picture;

    public TakePictureMission(int index){
        super("takePhoto", index);
    }

    @Override
    public void start() {
        Logger.debug("start take photo");
        SettingsDefinitions.ShootPhotoMode photoMode = SettingsDefinitions.ShootPhotoMode.SINGLE;
        handler = new Handler();
        camera.setShootPhotoMode(photoMode,new CommonCallbacks.CompletionCallback(){
            @Override
            public void onResult(DJIError djiError) {
                if (null == djiError) {
                    Logger.debug("set camara take photo ");
                    handler.postDelayed(new Runnable() {
                        @Override
                        public void run() {
                            Logger.debug("try take photo ");
                            camera.startShootPhoto(new CommonCallbacks.CompletionCallback() {
                                @Override
                                public void onResult(DJIError djiError) {
                                    if (djiError == null) {
                                        Logger.debug("take photo: success");
                                    } else {
                                        Logger.error(djiError.getDescription());
                                    }
                                }
                            });
                        }
                    }, 2000);
                }
            }
        });




    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done" + " " + size + " " + picture;
    }
}
