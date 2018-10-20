package BL.missions;

public class MoveCameraGimbalMission extends Mission {

    private int degriseVertical;
    private int degreeseOrisental;

    public MoveCameraGimbalMission(int degriseVertical, int degreeseOrisental){
        super("moveCameraGimbal");
        this.degriseVertical=degriseVertical;
        this.degreeseOrisental=degreeseOrisental;
    }
    @Override
    void start() {

    }

    @Override
    void stop() {

    }
}
