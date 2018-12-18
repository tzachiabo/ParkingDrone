package BL;

import java.util.Timer;
import java.util.TimerTask;

import BL.missions.Mission;
import SharedClasses.Logger;

public class SuperTimer extends Timer {

    Mission mission;
    TimerTask task;
    long interval;
    long totalTime;

    public SuperTimer(TimerTask task, Mission mission, long interval , long totalTime){
        super();
        this.task = task;
        this.interval = interval;
        this.totalTime = totalTime;
        this.mission = mission;
    }
    public void schedule(){
        super.schedule(new MissionTimerTask(this),0,interval);
    }

    private class MissionTimerTask extends TimerTask
    {
        SuperTimer superTimer;
        int counter;

        private MissionTimerTask(SuperTimer superTimer){
            this.superTimer = superTimer;
            this.counter = 0;
        }

        @Override
        public void run() {
            if(counter * interval > totalTime)
            {
                superTimer.cancel();
                Logger.info("a mission "+ mission.getName()+" was ended");
                try {
                    Thread.sleep(300);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                mission.getOnResult().onResult(null);
            }
            else
            {
                task.run();
            }

            counter++;
        }
    }

}


