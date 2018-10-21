package BL.missions;

public class StopMission extends Mission {
    public StopMission(int index){
        super("stop", index);
    }
    @Override
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + "Done";
    }
}
