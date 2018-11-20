package BL;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.Socket;
import java.net.UnknownHostException;

import BL.missions.Mission;
import SharedClasses.Config;
import SharedClasses.Logger;

public class SocketManager {

    private static SocketManager manager = null;
    Socket socket = null;
    InputStream inputStream;
    OutputStream outputStream;
    TaskManager taskManager;

    //Constants
    final  int BUFFER_SIZE = 1024;

    public static SocketManager getInstance(){
        if (manager == null)
        {
            manager = new SocketManager();
        }
        return manager;
    }

    private SocketManager(){
        taskManager = TaskManager.getInstance();

        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    ByteArrayOutputStream byteArrayOutputStream;
                    socket = new Socket(Config.DST_ADDRESS, Config.DST_PORT);
                    byteArrayOutputStream = new ByteArrayOutputStream(
                            BUFFER_SIZE);
                    byte[] buffer = new byte[BUFFER_SIZE];
                    int bytesRead;
                    inputStream = socket.getInputStream();
                    outputStream = socket.getOutputStream();
                    Logger.info("Connected to server");
                    while ((bytesRead = inputStream.read(buffer)) != -1) {
                        byteArrayOutputStream.write(buffer, 0, bytesRead);
                        Mission current_task = Decoder.decode(byteArrayOutputStream.toString("UTF-8"));
                        Logger.debug("Mission Recived - "+ byteArrayOutputStream.toString());
                        taskManager.addTask(current_task);
                        byteArrayOutputStream = new ByteArrayOutputStream(BUFFER_SIZE);
                        buffer = new byte[BUFFER_SIZE];
                    }

                } catch (UnknownHostException e) {
                    e.printStackTrace();
                } catch (IOException e) {
                    e.printStackTrace();

                } finally {
                    if (socket != null) {
                        try {
                            socket.close();
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                }

            }
        }).start();

    }

    public synchronized void send(String data){

        Logger.debug("sending to server "+data);
        try {
            data +="%";
            outputStream.write(data.getBytes("UTF-8"));
            outputStream.flush();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void close_socket() throws IOException {
        socket.close();
    }

}
