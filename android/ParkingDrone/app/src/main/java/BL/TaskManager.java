package BL;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import BL.missions.GetDroneStatusMission;
import BL.missions.GetGPSLocationMission;
import BL.missions.Mission;
import SharedClasses.Assertions;
import dji.thirdparty.afinal.core.AsyncTask;

public class TaskManager {
    private static TaskManager instance = null;
    ExecutorService MainExecutor;
    ExecutorService StatusExecutor;

    private TaskManager(){
        MainExecutor = Executors.newSingleThreadExecutor();
        StatusExecutor = Executors.newSingleThreadExecutor();
    }
    public static TaskManager getInstance(){
        if(instance == null){
            instance = new TaskManager();
        }
        return instance;
    }

    private void addMission(ExecutorService executor, final Mission mission){
        executor.submit(new Runnable() {
            @Override
            public void run() {
                mission.start();
            }
        });
    }

    private void addMainMission(Mission mission){
        addMission(MainExecutor, mission);
    }

    private void addStatusMission(Mission mission){
        addMission(StatusExecutor, mission);
    }

    public void addTask(Mission mission){
        if (mission instanceof GetDroneStatusMission || mission instanceof GetGPSLocationMission){
            addStatusMission(mission);
        }
        else
        {
            addMainMission(mission);
        }
    }

    public void stopAllTasks(){
        MainExecutor.shutdownNow();
    }
}
