package SharedClasses;

public interface Config {
    float BASE_SPEED = 2;
    long MOVMENT_BASE_INTERVAL = 100;
    String DST_ADDRESS = "192.168.43.154";
    int DST_PORT = 3000;
    String MAIN_CAMERA_NAME = "Zenmuse X5S";
    String DJI_PHOTO_DIR = "DJI_Photo";
    int TIME_OF_GIMBAL_MOVE = 2;//seconds
    int MAX_TIME_WAIT_FOR_LANDING = 10000;//ms
    int MAX_TIME_WAIT_FOR_TAKEOFF = 5000;//ms
}