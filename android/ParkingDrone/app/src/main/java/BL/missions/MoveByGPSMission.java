package BL.missions;

public class MoveByGPSMission extends Mission {

    private double xLoc;
    private double yLoc;
    private double zLOC;

    public MoveByGPSMission(int index, double x, double y, double z){
        super("moveByGPS", index);
        xLoc=x;
        yLoc=y;
        zLOC=z;
    }

    @Override
    void start() {

    }

    @Override
    void stop() {

    }
}
