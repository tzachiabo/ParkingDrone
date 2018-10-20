package BL.missions;

public class GetGPSLocationMission extends Mission {
    int LocX;
    int LocY;
    int LocZ;
    public GetGPSLocationMission(int index){
        super("getLocation", index);
    }

    @Override
    void start() {

    }

    @Override
    void stop() {

    }

    @Override
    String encode() {
        return getName() +" "+ getIndex()+ " " + LocX + " " + LocY + " " + LocZ;
    }
}
