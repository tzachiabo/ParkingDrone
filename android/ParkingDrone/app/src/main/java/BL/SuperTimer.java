package BL;

import java.util.Timer;
import java.util.TimerTask;

import BL.Drone.DJIM210.M210Manager;
import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import BL.missions.Mission;
import SharedClasses.AngularHelper;
import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.LatLngHelper;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.flightcontroller.LocationCoordinate3D;
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
    public void scheduleMoveUpTask(double targetHeight){
        super.schedule(new MoveUpTask(this, targetHeight),0,interval);
    }
    public void scheduleMoveDownTask(double targetHeight){
        super.schedule(new MoveDownTask(this, targetHeight),0,interval);
    }
    public void scheduleSmartMoveTask(double distance){
        super.schedule(new SmartMoveTask(this, distance),0,interval);
    }


    private class MissionTimerTask extends TimerTask
    {
        SuperTimer superTimer;
        int counter;
        private boolean is_finished;

        private MissionTimerTask(SuperTimer superTimer){
            this.superTimer = superTimer;
            this.counter = 0;
            is_finished = false;
        }

        @Override
        public void run() {
            if(counter * interval > totalTime)
            {
                boolean local_is_finished = is_finished;
                is_finished = true;
                superTimer.cancel();
                hoverCommand();
                Logger.info("a mission "+ mission.getName()+" was ended");
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                if (!local_is_finished) {
                    mission.getOnResult().onResult(null);
                }
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
            double angularDistance = AngularHelper.angularDistance(currentBearing, targetBearing);
            Logger.info("BEARING: angular distance is : " + angularDistance + "previous is : " + previousAngularDistance);
            if(angularDistance < Config.BEARING_APRROXIMATION || previousAngularDistance < angularDistance)
            {
                superTimer.cancel();
                hoverCommand();
                Logger.info("BEARING : canceling timer");
                Logger.info("a mission "+ mission.getName()+" was ended");
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                mission.getOnResult().onResult(null);
            }
            else
            {
                previousAngularDistance = AngularHelper.angularDistance(currentBearing, targetBearing);
                task.run();
            }
        }
    }

    private class MoveUpTask extends TimerTask
    {
        SuperTimer superTimer;
        double targetHeight;
        FlightController flightController;

        private MoveUpTask(SuperTimer superTimer, double targetHeight){
            this.superTimer = superTimer;
            this.targetHeight = targetHeight;
            Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
            this.flightController = aircraft.getFlightController();
        }

        @Override
        public void run() {
            IDrone drone = DroneFactory.getDroneManager();
            double currentHeight = drone.getDroneState().getAircraftLocation().getAltitude();
            if(currentHeight > targetHeight - Config.Movement_APRROXIMATION)
            {
                superTimer.cancel();
                hoverCommand();
                Logger.info("BEARING : canceling timer");
                Logger.info("a mission "+ mission.getName()+" was ended");
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                mission.getOnResult().onResult(null);
            }
            else
            {
                task.run();
            }
        }
    }

    private class MoveDownTask extends TimerTask
    {
        SuperTimer superTimer;
        double targetHeight;
        FlightController flightController;

        private MoveDownTask(SuperTimer superTimer, double targetHeight){
            this.superTimer = superTimer;
            this.targetHeight = targetHeight;
            Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
            this.flightController = aircraft.getFlightController();
        }

        @Override
        public void run() {
            IDrone drone = DroneFactory.getDroneManager();
            double currentHeight = drone.getDroneState().getAircraftLocation().getAltitude();
            if(currentHeight < targetHeight + Config.Movement_APRROXIMATION)
            {
                superTimer.cancel();
                hoverCommand();
                Logger.info("BEARING : canceling timer");
                Logger.info("a mission "+ mission.getName()+" was ended");
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                mission.getOnResult().onResult(null);
            }
            else
            {
                task.run();
            }
        }
    }

    private class SmartMoveTask extends TimerTask
    {
        SuperTimer superTimer;
        double distance;
        LocationCoordinate3D source_location;
        FlightController flightController;

        private SmartMoveTask(SuperTimer superTimer, double distance){
            this.superTimer = superTimer;
            this.distance = distance;
            IDrone drone = DroneFactory.getDroneManager();
            this.source_location = drone.getDroneState().getAircraftLocation();
            Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
            this.flightController = aircraft.getFlightController();
        }

        @Override
        public void run() {
            IDrone drone = DroneFactory.getDroneManager();
            LocationCoordinate3D currentLocation = drone.getDroneState().getAircraftLocation();
            double distance_passed = LatLngHelper.getDistanceBetweenTwoPoints(currentLocation, source_location);
            Logger.info("distance passed " + distance_passed);

            if(distance_passed > distance - Config.Movement_APRROXIMATION)
            {
                superTimer.cancel();
                hoverCommand();
                Logger.info("a mission "+ mission.getName()+" was ended");
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                mission.getOnResult().onResult(null);
            }
            else
            {
                task.run();
            }
        }
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


