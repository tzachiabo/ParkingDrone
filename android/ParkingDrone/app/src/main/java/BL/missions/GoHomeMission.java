package BL.missions;

import SharedClasses.Assertions;

public class GoHomeMission extends Mission {

    public GoHomeMission(int index){
        super("goHome", index);
    }

    @Override
    public void start() {
        Assertions.verify(false, "go home is not implemented yet");
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex()+ " " + "Done";
    }
}
