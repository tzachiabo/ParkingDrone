package BL;

import java.io.BufferedReader;
import java.io.IOException;

public class SocketReader extends Thread {
    private BufferedReader reader;

    public SocketReader(BufferedReader reader){
        this.reader = reader;
    }

    @Override
    public void run(){
        while(true){
            try {
                String message = reader.readLine();
                SocketManager.getInstance().send(message);

            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }



}
