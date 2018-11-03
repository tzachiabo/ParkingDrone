package BL.missions;

import android.os.Handler;

import com.dji.mapkit.core.camera.DJICameraUpdateFactory;

import SharedClasses.RemoteLogCat;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.*;
import dji.common.camera.*;
import dji.sdk.products.Aircraft;
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
        SettingsDefinitions.ShootPhotoMode photoMode = SettingsDefinitions.ShootPhotoMode.SINGLE;
        handler = new Handler();
        camera.setShootPhotoMode(photoMode,new CommonCallbacks.CompletionCallback(){
            @Override
            public void onResult(DJIError djiError) {
                if (null == djiError) {
                    handler.postDelayed(new Runnable() {
                        @Override
                        public void run() {
                            camera.startShootPhoto(new CommonCallbacks.CompletionCallback() {
                                @Override
                                public void onResult(DJIError djiError) {
                                    if (djiError == null) {
                                        RemoteLogCat.debug("take photo: success");
                                    } else {
                                        RemoteLogCat.error(djiError.getDescription());
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
