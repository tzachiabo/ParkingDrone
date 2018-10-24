package BL.missions;

import BL.SocketManager;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;

public abstract class Mission {
    private String name;
    private int index;
    MissionReport onResult;

    public Mission(String name, int index){
        this.name=name;
        this.index=index;
        this.onResult = new MissionReport();
    }
    public MissionReport getOnResult(){
        return onResult;
    }
    public abstract void start();
    public abstract void stop();
    public abstract String encode();
    public String getName(){
        return name;
    }
    public int getIndex(){return index;}

    public class MissionReport implements CommonCallbacks.CompletionCallback {

        @Override
        public void onResult(DJIError djiError) {
            SocketManager.getInstance().send(Mission.this.encode());
        }
    }
}
