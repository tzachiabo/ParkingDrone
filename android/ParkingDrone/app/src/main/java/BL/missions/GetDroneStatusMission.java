package BL.missions;

public class GetDroneStatusMission extends Mission {
    int status;
    public GetDroneStatusMission(int index){
      super("getStatus", index);
    }
    @Override
    void start() {

    }

    @Override
    void stop() {

    }

    @Override
    String encode() {
        return getName() +" "+ getIndex() + " " + status;
    }
}
