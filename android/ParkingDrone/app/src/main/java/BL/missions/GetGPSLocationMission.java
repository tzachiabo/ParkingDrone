package BL.missions;

import SharedClasses.Assertions;

public class GetGPSLocationMission extends Mission {
    int LocX;
    int LocY;
    int LocZ;
    public GetGPSLocationMission(int index){
        super("getLocation", index);
    }

    @Override
    public void start() {
        Assertions.verify(false, "get location is not implemented yet");
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex()+ " " + LocX + " " + LocY + " " + LocZ;
    }
}
