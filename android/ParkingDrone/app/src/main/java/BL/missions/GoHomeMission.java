package BL.missions;

public class GoHomeMission extends Mission {

    public GoHomeMission(int index){
        super("goHome", index);
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
