package BL.missions;
import SharedClasses.Direction;

public class MoveMission extends Mission {

    private Direction direction;
    private int distance;
    public MoveMission(Direction direction, int distance){
        super("move");
        this.direction=direction;
        this.distance=distance;
    }
    @Override
    void start() {

    }

    @Override
    void stop() {

    }
}
