package BL;

import java.util.concurrent.ConcurrentHashMap;

import BL.missions.Mission;
import SharedClasses.Assertions;
import dji.thirdparty.afinal.core.AsyncTask;

public class TaskManager {
    private static TaskManager instance = null;
    private final ConcurrentHashMap<Integer,Mission> running_missions = new ConcurrentHashMap<>();

    private TaskManager(){

    }
    public static TaskManager getInstance(){
        if(instance == null){
            instance = new TaskManager();
        }
        return instance;
    }

    public void addTask(Mission mission){
        Assertions.verify(!this.running_missions.containsKey(mission.getIndex()),
                "TaskManager: task allready exist in the task queue");

        this.running_missions.put(mission.getIndex(), mission);
    }
    public boolean removeTask(Integer id){
        if(this.running_missions.containsKey(id)){
            this.running_missions.remove(id);
            return true;
        }
        return false;
    }
    public boolean removeTask(Mission mission){
        if(this.running_missions.containsKey(mission.getIndex())){
            this.running_missions.remove(mission.getIndex());
            return true;
        }
        return false;
    }
    public void stopAllTasks(){
        for(Mission mission : this.running_missions.values()){
            mission.stop();
            removeTask(mission);
        }
    }
    public void start(final Integer mission_id) {
        Assertions.verify(this.running_missions.containsKey(mission_id),
                "TaskManager:start mission is not exist in queue");
        //inner_exec();


                    Thread t= new Thread(new Runnable() {
            @Override
            public void run() {
                running_missions.get(mission_id).start();
            }
        });
        t.start();
    }

    private static void inner_exec(final ConcurrentHashMap<Integer,Mission> running_missions, final int mission_id){
        new android.os.AsyncTask<Void, Void, String>() {

            @Override
            protected String doInBackground(Void... voids) {
                running_missions.get(mission_id).start();
                return "";
            }
        }.execute();
    }
}
