package BL.missions;

import android.os.Handler;

import com.example.aviad.parkingdrone.MainActivity;

import BL.BLManager;
import SharedClasses.Config;
import SharedClasses.Logger;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;
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
    String pic_name;
    public static Camera camera;

    public TakePictureMission(int index) {
        super("takePhoto", index);
    }

    @Override
    public void start() {
        Logger.debug("start take photo");
        sendFileToServer("pic_" + MainActivity.current_pic + ".jpg");
    }


    @Override
    public void stop() {

    }

    private void sendFileToServer(String file_name){
        File pic = new File(BLManager.getInstance().file, file_name);
        Logger.debug("start read pic from android memory");

        try {
            int size = (int) pic.length();
            byte[] bytes = new byte[size];

            BufferedInputStream buf = new BufferedInputStream(new FileInputStream(pic));
            buf.read(bytes, 0, bytes.length);
            buf.close();
            Logger.debug("finish read pic from android memory");
            sendPic(bytes);

        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendPic(final byte[] data) throws IOException {
        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    Logger.debug("start send pic to server");
                    Socket socket = new Socket(Config.DST_ADDRESS, 3001);
                    OutputStream outputStream = socket.getOutputStream();
                    outputStream.write(data);
                    outputStream.flush();
                    socket.close();
                    Logger.debug("finish send pic to server");
                    onResult.onResult(null);
                } catch (IOException e) {
                    e.printStackTrace();
                }

            }
        }).start();
    }

    @Override
    public String encode() {
        return getName() + " " + getIndex() + " " + "Done" + " " + pic_name+".JPG";
    }
}
