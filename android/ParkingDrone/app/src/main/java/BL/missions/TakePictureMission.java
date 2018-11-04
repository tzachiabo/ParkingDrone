package BL.missions;

import android.os.Handler;
import SharedClasses.Logger;


import java.util.List;

import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.*;
import dji.sdk.media.MediaFile;

public class TakePictureMission extends Mission {
    private Handler handler;
    int size;
    byte[] picture;
    public static Camera camera;

    public TakePictureMission(int index) {
        super("takePhoto", index);
    }

    @Override
    public void start() {
        Logger.debug("start take photo");
        SettingsDefinitions.ShootPhotoMode photoMode = SettingsDefinitions.ShootPhotoMode.SINGLE;
        handler = new Handler();
        camera.setShootPhotoMode(photoMode,new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                camera.startShootPhoto(new CommonCallbacks.CompletionCallback() {
                    @Override
                    public void onResult(DJIError djiError) {
                        if (djiError == null) {
                            try {
                                Thread.sleep(2000);
                            } catch (InterruptedException e) {
                                e.printStackTrace();
                            }
                            Logger.debug("take photo: success");
                            Logger.debug("internal storage list state : " + camera.getMediaManager().getSDCardFileListState());
                            List<MediaFile> sdCardFileListSnapshot = camera.getMediaManager().getSDCardFileListSnapshot();
                            Logger.debug("number of files in fs :" + sdCardFileListSnapshot.size());
                            for (MediaFile file : sdCardFileListSnapshot) {
                                Logger.debug("filename: " + file.getFileName());
                            }
                        } else {
                            Logger.error("take photo result in error : " + djiError.getDescription());
                        }
                    }

                });
            }});
        }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() + " " + getIndex() + " " + "Done" + " " + size + " " + picture;
    }
}
