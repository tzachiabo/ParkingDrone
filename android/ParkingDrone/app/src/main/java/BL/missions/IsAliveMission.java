package BL.missions;

public class IsAliveMission extends Mission {

    public  IsAliveMission(int index){
        super("isAlive", index);
    }

    @Override
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex()+ " " + "Done";
    }
}
