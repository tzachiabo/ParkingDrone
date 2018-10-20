package BL.missions;

public abstract class Mission {
    private String name;
    private int index;
    public Mission(String name, int index){
        this.name=name;
        this.index=index;
    }
    abstract void start();
    abstract void stop();
    abstract String encode();
    public String getName(){
        return name;
    }
    public int getIndex(){return index;}
}
