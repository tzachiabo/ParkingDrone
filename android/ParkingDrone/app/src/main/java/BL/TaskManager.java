package BL;

import android.util.Log;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;

import BL.missions.GetDroneStatusMission;
import BL.missions.GetGPSLocationMission;
import BL.missions.Mission;
import BL.missions.StopMission;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.thirdparty.afinal.core.AsyncTask;

public class TaskManager {
    private static TaskManager instance = null;
    ThreadPoolExecutor MainExecutor;
    ThreadPoolExecutor StatusExecutor;
    ThreadPoolExecutor LocationExecutor;
    ThreadPoolExecutor StopExecutor;
    Mission currentMission;
    public AtomicInteger TOTAL_NUM_OF_TASKS;
    public AtomicInteger NUM_OF_DONE_TASKS;

    private TaskManager(){
        MainExecutor = new ThreadPoolExecutor(1, 1, 0L,
                TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
        StatusExecutor = new ThreadPoolExecutor(1, 1, 0L,
                TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
        StopExecutor = new ThreadPoolExecutor(1, 1, 0L,
                TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
        LocationExecutor = new ThreadPoolExecutor(1, 1, 0L,
                TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
        TOTAL_NUM_OF_TASKS = new AtomicInteger();
        NUM_OF_DONE_TASKS = new AtomicInteger();
    }
    public static TaskManager getInstance(){
        if(instance == null){
            instance = new TaskManager();
        }
        return instance;
    }

    private void addMission(ThreadPoolExecutor executor, final Mission mission){
        try {
            Logger.info("TOTAL NUM OF TASKS " + TOTAL_NUM_OF_TASKS.incrementAndGet());
            synchronized(executor) {
                Logger.info("num of job of executor is " + executor.getQueue().size());
                executor.submit(new Runnable() {
                    @Override
                    public void run() {
                        try{
                            mission.run();
                        }
                        catch (Exception e){
                            Logger.fatal("mission "+ mission.getName() + ":"+ mission.getIndex()+ " failed");
                        }
                    }
                });
            }
        }
        catch (Exception e){
            Logger.fatal("failed to submit mission " + mission.getName() + " num : " + mission.getIndex());
        }
    }

    private void addMainMission(Mission mission){
        //todo:assertion empty executor
        currentMission = mission;
        addMission(MainExecutor, mission);
    }
    private void addStopMission(Mission mission){
        addMission(StopExecutor, mission);
    }

    private void addStatusMission(Mission mission){
        addMission(StatusExecutor, mission);
    }

    private void addLocationMission(Mission mission){
        addMission(LocationExecutor, mission);
    }

    public void addTask(Mission mission){
        if(mission instanceof StopMission){
            addStopMission(mission);
        }
        else if (mission instanceof GetGPSLocationMission){
            addStatusMission(mission);
        }
        else if (mission instanceof GetDroneStatusMission){
            addLocationMission(mission);
        }
        else
        {
            addMainMission(mission);
        }
    }

    public void stopAllTasks(){
        currentMission.stop();
    }
}
