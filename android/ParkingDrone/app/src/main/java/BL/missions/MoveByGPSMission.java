package BL.missions;

public class MoveByGPSMission extends Mission {

    private int xLoc;
    private  int yLoc;

    public MoveByGPSMission(int x,int y){
        super("moveByGPS");
        xLoc=x;
        yLoc=y;
    }

    @Override
    void start() {

    }

    @Override
    void stop() {

    }
}
