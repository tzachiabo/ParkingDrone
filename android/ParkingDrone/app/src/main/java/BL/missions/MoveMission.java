package BL.missions;
import SharedClasses.Direction;

public class MoveMission extends Mission {

    private Direction direction;
    private double distance;
    public MoveMission(int index, Direction direction, double distance){
        super("move", index);
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
