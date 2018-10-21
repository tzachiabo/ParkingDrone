package BL.missions;

public class LandingMission extends Mission {

    public LandingMission(int index){
        super("landing", index);
    }

    @Override
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }
}
