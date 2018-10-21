package BL.missions;

public class GetGPSLocationMission extends Mission {
    int LocX;
    int LocY;
    int LocZ;
    public GetGPSLocationMission(int index){
        super("getLocation", index);
    }

    @Override
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex()+ " " + LocX + " " + LocY + " " + LocZ;
    }
}
