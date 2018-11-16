package BL.missions;

import android.os.Handler;
import BL.BLManager;
import SharedClasses.Logger;
import java.util.List;
import dji.common.camera.SettingsDefinitions;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.camera.*;
import dji.sdk.media.DownloadListener;
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
        camera.startShootPhoto(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError == null) {
                    try {
                        Thread.sleep(2000);//TODO::check if needed
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    Logger.debug("take photo: success");
                    camera.getMediaManager().refreshFileListOfStorageLocation(SettingsDefinitions.StorageLocation.SDCARD, new CommonCallbacks.CompletionCallback() {
                        @Override
                        public void onResult(DJIError djiError) {

                            if(djiError != null){
                                Logger.debug("refreshFileListOfStorageLocation result in error : "+djiError.toString());
                            }
                            else{
                                Logger.debug("sd storage list state : " + camera.getMediaManager().getSDCardFileListState());
                                List<MediaFile> sdCardFileListSnapshot = camera.getMediaManager().getSDCardFileListSnapshot();
                                Logger.debug("number of files in fs :" + sdCardFileListSnapshot.size());

                                MediaFile file = sdCardFileListSnapshot.get(0);
                                file.fetchFileData(BLManager.getInstance().file,"basePhoto", new DownloadListener<String>() {
                                    @Override
                                    public void onStart() {
                                        Logger.info("fetching file");
                                    }

                                    @Override
                                    public void onRateUpdate(long l, long l1, long l2) {

                                    }

                                    @Override
                                    public void onProgress(long l, long l1) {

                                    }

                                    @Override
                                    public void onSuccess(String s) {
                                        Logger.info("fetching file Success");


                                    }

                                    @Override
                                    public void onFailure(DJIError djiError) {
                                        Logger.info("fetching file failed error: "+ djiError.toString());
                                    }
                                });

                            }
                        }
                    });

                } else {
                    Logger.error("take photo result in error : " + djiError.getDescription());
                }
            }

        });

        }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() + " " + getIndex() + " " + "Done" + " " + size + " " + picture;
    }
}
