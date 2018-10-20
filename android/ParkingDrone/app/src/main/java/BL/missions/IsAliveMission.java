package BL.missions;

public class IsAliveMission extends Mission {

    public  IsAliveMission(int index){
        super("isAlive", index);
    }

    @Override
    void start() {

    }

    @Override
    void stop() {

    }

    @Override
    String encode() {
        return getName() +" "+ getIndex()+ " " + "Done";
    }
}
