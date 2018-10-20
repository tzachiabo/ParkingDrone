package BL.missions;

public class GoHomeMission extends Mission {

    public GoHomeMission(int index){
        super("goHome", index);
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
