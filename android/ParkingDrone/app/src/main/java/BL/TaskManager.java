package BL;

import java.util.concurrent.ConcurrentHashMap;

import BL.missions.Mission;
import SharedClasses.Assertions;

public class TaskManager {
    private static TaskManager instance = null;
    private ConcurrentHashMap<Integer,Mission> running_missions = null;

    private TaskManager(){
        running_missions = new ConcurrentHashMap<>();
    }
    public static TaskManager getInstance(){
        if(instance == null){
            instance = new TaskManager();
        }
        return instance;
    }

    public void addTask(Mission mission){
        Assertions.verify(this.running_missions.containsKey(mission.getIndex()),
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
    public void start(Integer mission_id) {
        Assertions.verify(this.running_missions.containsKey(mission_id),
                "TaskManager:start mission is not exist in queue");

        this.running_missions.get(mission_id).start();
    }
}
