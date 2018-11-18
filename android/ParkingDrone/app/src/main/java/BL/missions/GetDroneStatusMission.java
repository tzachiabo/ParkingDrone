package BL.missions;

import BL.BLManager;
import SharedClasses.Assertions;
import SharedClasses.DroneStatus;

public class GetDroneStatusMission extends Mission {
    DroneStatus status;
    public GetDroneStatusMission(int index){
      super("getStatus", index);
    }
    @Override
    public void start() {
        status = BLManager.getInstance().getDroneStatus();

        onResult.onResult(null);
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " Done " + status;
    }
}
