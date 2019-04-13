package BL;

import java.util.Timer;
import java.util.TimerTask;

import BL.Drone.DJIM210.M210Manager;
import BL.missions.Mission;
import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.flightcontroller.virtualstick.FlightControlData;
import dji.common.util.CommonCallbacks;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

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
    public void scheduleBearingTask(double targetBearing){
        super.schedule(new BearingTimerTask(this, targetBearing),0,interval);
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

    private class BearingTimerTask extends TimerTask
    {
        SuperTimer superTimer;
        double targetBearing;
        double previousAngularDistance;
        FlightController flightController;
        private BearingTimerTask(SuperTimer superTimer, double targetBearing){
            this.superTimer = superTimer;
            this.targetBearing = targetBearing;
            this.previousAngularDistance = 360;
            Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
            this.flightController = aircraft.getFlightController();
        }

        @Override
        public void run() {
            float currentBearing = flightController.getCompass().getHeading();
            double angularDistance = angularDistance(currentBearing, targetBearing);
            Logger.info("BEARING: angular distance is : " + angularDistance + "previous is : " + previousAngularDistance);
            if(angularDistance < Config.BEARING_APRROXIMATION || previousAngularDistance < angularDistance)
            {
                superTimer.cancel();
                hoverCommand();
                Logger.info("BEARING : canceling timer");
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
                previousAngularDistance = angularDistance(currentBearing, targetBearing);
                task.run();
            }
        }
    }

    private static double toRads(double degrees)
    {
        return degrees * Math.PI / 180;
    }
    private static double toDegrees(double rads)
    {
        return rads * 180 / Math.PI;
    }

    private static double angularDistance(double destBearing, double currBearing)
    {
        destBearing = toRads(destBearing);
        currBearing = toRads(currBearing);
        return Math.abs(toDegrees(Math.atan2(Math.sin(destBearing - currBearing), Math.cos(destBearing - currBearing))));
    }

    private void hoverCommand() {
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        FlightControlData fcd = new FlightControlData(0, 0, 0, 0);
        aircraft.getFlightController().sendVirtualStickFlightControlData(fcd, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                try {
                    if (djiError != null) {
                        Logger.error("after move djierror is " + djiError.toString());
                        Assertions.verify(false, "failed to move drone");
                    }
                }
                catch (AssertionViolation e){
                    Logger.fatal("catch - failed to move drone with dji error");
                }
            }
        });
    }

}


