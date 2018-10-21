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
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + "Done";
    }
}
