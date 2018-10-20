package BL.missions;

public abstract class Mission {
    private String name;
    public Mission(String name){
        this.name=name;
    }
    abstract void start();
    abstract void stop();
    public String getName(){
        return name;
    }
}
