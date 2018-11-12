package BL.missions;

import SharedClasses.Assertions;

public class GetDroneStatusMission extends Mission {
    int status;
    public GetDroneStatusMission(int index){
      super("getStatus", index);
    }
    @Override
    public void start() {
        Assertions.verify(false, "get status is not implemented yet");
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + status;
    }
}
