package BL.missions;

import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;

public class TakeQuickPictureMission extends Mission {

    public TakeQuickPictureMission(int index) {
        super("takeQuickPhoto", index);
    }

    @Override
    public void start() {
        Logger.debug("start quick take photo");
        IDrone drone = DroneFactory.getDroneManager();
        byte[] img = drone.getQuickImage();
        sendPic(img);
    }


    @Override
    public void stop() {

    }

    private void sendPic(final byte[] data) {
        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    Logger.debug("start send pic to server from quick photo");
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
        return getName() + " " + getIndex() + " " + "Done";
    }
}
