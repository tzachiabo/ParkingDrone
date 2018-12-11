package BL.missions;

import BL.SocketManager;
import BL.TaskManager;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;

public abstract class Mission implements Runnable {
    private String name;
    protected int index;
    MissionReport onResult;
    boolean hasStoped;
    boolean isMissionCompleted;

    public Mission(String name, int index){
        this.name=name;
        this.index=index;
        this.onResult = new MissionReport();
        isMissionCompleted = false;
        hasStoped = false;
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
    public int getIndex() { return index; }
    public void run(){
        start();
    }

    public class MissionReport implements CommonCallbacks.CompletionCallback {

        @Override
        public void onResult(DJIError djiError) {
            isMissionCompleted = true;
            SocketManager.getInstance().send(Mission.this.encode());
        }
    }
}
