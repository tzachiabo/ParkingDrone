package BL.missions;

public class TakePictureMission extends Mission {

    int size;
    byte[] picture;

    public TakePictureMission(int index){
        super("takePhoto", index);
    }

    @Override
    public void start() {

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done" + " " + size + " " + picture;
    }
}
