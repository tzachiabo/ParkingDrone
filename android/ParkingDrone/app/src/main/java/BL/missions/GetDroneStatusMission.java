package BL.missions;

public class GetDroneStatusMission extends Mission {
    int status;
    public GetDroneStatusMission(int index){
      super("getStatus", index);
    }
    @Override
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + status;
    }
}
