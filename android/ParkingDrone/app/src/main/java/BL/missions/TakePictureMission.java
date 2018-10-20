package BL.missions;

public class TakePictureMission extends Mission {

    int size;
    byte[] picture;

    public TakePictureMission(int index){
        super("takePhoto", index);
    }

    @Override
    void start() {

    }

    @Override
    void stop() {

    }

    @Override
    String encode() {
        return getName() +" "+ getIndex() + " " + "Done" + " " + size + " " + picture;
    }
}
