package BL.missions;

public class MoveCameraGimbalMission extends Mission {

    private double VerDegree;
    private double HorDegree;

    public MoveCameraGimbalMission(int index, double VerDegree, double HorDegree){
        super("moveGimbal", index);
        this.VerDegree=VerDegree;
        this.HorDegree=HorDegree;
    }
    @Override
    void start() {

    }

    @Override
    void stop() {

    }

    @Override
    String encode() {
        return getName() +" "+ getIndex() + "Done";
    }
}
