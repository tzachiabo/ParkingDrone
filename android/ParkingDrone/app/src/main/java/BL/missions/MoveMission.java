package BL.missions;
import java.util.TimerTask;

import BL.Drone.DJIM210.M210Manager;
import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.AssertionViolation;
import SharedClasses.Config;
import BL.SuperTimer;
import SharedClasses.Assertions;
import SharedClasses.Direction;
import SharedClasses.LatLngHelper;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.flightcontroller.LocationCoordinate3D;
import dji.common.flightcontroller.virtualstick.FlightControlData;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class MoveMission extends Mission {

    private Direction direction;
    private double distance;
    private SuperTimer st;

    public MoveMission(int index, Direction direction, double distance){
        super("move", index);
        this.direction=direction;
        this.distance=distance;
    }

    @Override
    public void start() {
        switch (direction){
            case rtt_left:
            case rtt_right:
                rotate();
                break;
            case left:
            case right:
            case forward:
            case backward:
                smart_normal_move();
                break;
            case up:
                move_up();
                break;
            case down:
                move_down();
                break;
        }
    }

    @Override
    public void stop() {
        if (!isMissionCompleted) {
            st.cancel();
            Logger.warn("move mission number " + getIndex() + " has stop");
        }
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }

    private FlightControlData getFCB(){
        FlightControlData fcd = null;
        switch (direction){
            case forward:
                fcd = new FlightControlData(0,Config.BASE_SPEED,0,0);
                break;
            case right:
                fcd = new FlightControlData(Config.BASE_SPEED,0,0,0);
                break;
            case left:
                fcd = new FlightControlData(-Config.BASE_SPEED,0,0,0);
                break;
            case backward:
                fcd = new FlightControlData(0,-Config.BASE_SPEED,0,0);
                break;
            case up:
                fcd = new FlightControlData(0,0,0,Config.BASE_SPEED);
                break;
            case down:
                fcd = new FlightControlData(0,0,0,-Config.BASE_SPEED);
                break;
            case rtt_right:
                fcd = new FlightControlData(0,0,Config.BASE_ANGULAR_SPEED, 0);
                break;
            case rtt_left:
                fcd = new FlightControlData(0,0,-Config.BASE_ANGULAR_SPEED, 0);
                break;
            default:
                Logger.error("Couldnt parse move direction");
                Assertions.verify(false, "getFCB is in unexpected flow");
                break;
        }

        return fcd;
    }

    private FlightControlData getFCBShort(){
        FlightControlData fcd = null;
        switch (direction){
            case forward:
                fcd = new FlightControlData(0,Config.BASE_SPEED_when_close,0,0);
                break;
            case right:
                fcd = new FlightControlData(Config.BASE_SPEED_when_close,0,0,0);
                break;
            case left:
                fcd = new FlightControlData(-Config.BASE_SPEED_when_close,0,0,0);
                break;
            case backward:
                fcd = new FlightControlData(0,-Config.BASE_SPEED_when_close,0,0);
                break;
            case up:
                fcd = new FlightControlData(0,0,0,Config.BASE_SPEED_when_close);
                break;
            case down:
                fcd = new FlightControlData(0,0,0,-Config.BASE_SPEED_when_close);
                break;
            case rtt_right:
                fcd = new FlightControlData(0,0,Config.BASE_ANGULAR_SPEED, 0);
                break;
            case rtt_left:
                fcd = new FlightControlData(0,0,-Config.BASE_ANGULAR_SPEED, 0);
                break;
            default:
                Logger.error("Couldnt parse move direction");
                Assertions.verify(false, "getFCB is in unexpected flow");
                break;
        }

        return fcd;
    }

    private FlightControlData getFCBVeryShort(){
        FlightControlData fcd = null;
        switch (direction){
            case forward:
                fcd = new FlightControlData(0,Config.BASE_SPEED_when_close / 2,0,0);
                break;
            case right:
                fcd = new FlightControlData(Config.BASE_SPEED_when_close / 2,0,0,0);
                break;
            case left:
                fcd = new FlightControlData(-Config.BASE_SPEED_when_close / 2,0,0,0);
                break;
            case backward:
                fcd = new FlightControlData(0,-Config.BASE_SPEED_when_close/ 2,0,0);
                break;
            case up:
                fcd = new FlightControlData(0,0,0,Config.BASE_SPEED_when_close/2);
                break;
            case down:
                fcd = new FlightControlData(0,0,0,-Config.BASE_SPEED_when_close/2);
                break;
            case rtt_right:
                fcd = new FlightControlData(0,0,Config.BASE_ANGULAR_SPEED, 0);
                break;
            case rtt_left:
                fcd = new FlightControlData(0,0,-Config.BASE_ANGULAR_SPEED, 0);
                break;
            default:
                Logger.error("Couldnt parse move direction");
                Assertions.verify(false, "getFCB is in unexpected flow");
                break;
        }

        return fcd;
    }

    public void move_up(){
        long totalTime = 1000000000;

        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        IDrone drone = DroneFactory.getDroneManager();
        double currentHeight = drone.getDroneState().getAircraftLocation().getAltitude();

        final double finalHeight = currentHeight + distance;
        st = new SuperTimer(new TimerTask() {
            @Override
            public void run() {
                IDrone drone = DroneFactory.getDroneManager();
                double currentHeight = drone.getDroneState().getAircraftLocation().getAltitude();

                double remainig_distance = finalHeight - currentHeight;
                FlightControlData fcd;
                if (remainig_distance > 5)
                {
                    fcd = getFCB();
                }
                else if (remainig_distance > 2)
                {
                    fcd = getFCBShort();
                }
                else
                {
                    fcd = getFCBVeryShort();
                }

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
        },this, Config.MOVMENT_BASE_INTERVAL, totalTime);


        st.scheduleMoveUpTask(finalHeight);
    }

    public void move_down(){
        long totalTime = 1000000000;

        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        IDrone drone = DroneFactory.getDroneManager();
        double currentHeight = drone.getDroneState().getAircraftLocation().getAltitude();

        final double finalHeight = currentHeight - distance;

        st = new SuperTimer(new TimerTask() {
            @Override
            public void run() {
                IDrone drone = DroneFactory.getDroneManager();
                double currentHeight = drone.getDroneState().getAircraftLocation().getAltitude();

                double remainig_distance = currentHeight - finalHeight;
                FlightControlData fcd;
                if (remainig_distance > 5)
                {
                    fcd = getFCB();
                }
                else if (remainig_distance > 2)
                {
                    fcd = getFCBShort();
                }
                else
                {
                    fcd = getFCBVeryShort();
                }

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
        },this, Config.MOVMENT_BASE_INTERVAL, totalTime);

        Assertions.verify(finalHeight > 0, "cannot move to less than zero height");

        st.scheduleMoveDownTask(finalHeight);
    }

    public void smart_normal_move()
    {
        distance -= 0.3;
        long totalTime = ((long)distance/(long)Config.BASE_ANGULAR_SPEED) * 1000;
        IDrone drone = DroneFactory.getDroneManager();
        final LocationCoordinate3D start_location = drone.getDroneState().getAircraftLocation();

        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();

        st = new SuperTimer(new TimerTask() {
            @Override
            public void run() {
                IDrone drone = DroneFactory.getDroneManager();
                LocationCoordinate3D currentLocation = drone.getDroneState().getAircraftLocation();

                double distance_passed = LatLngHelper.getDistanceBetweenTwoPoints(currentLocation, start_location);
                double remainig_distance = distance_passed - distance;
                FlightControlData fcd;
                if (remainig_distance > 5)
                {
                    fcd = getFCB();
                }
                else if (remainig_distance > 2)
                {
                    fcd = getFCBShort();
                }
                else
                {
                    fcd = getFCBVeryShort();
                }

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
        },this, Config.MOVMENT_BASE_INTERVAL, totalTime);

        st.scheduleSmartMoveTask(distance);
    }


   public void rotate() {
        long totalTime = ((long)distance/(long)Config.BASE_ANGULAR_SPEED) * 1000;
        totalTime -= 800;

        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();

        Logger.debug("start-move-mission "+"distance is "+distance+" totalTime is "+totalTime);
        st = new SuperTimer(new TimerTask() {
            @Override
            public void run() {
                FlightControlData fcd = getFCB();

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
        },this, Config.MOVMENT_BASE_INTERVAL, totalTime);

        double finalTarget;
        if(direction == Direction.rtt_left) {
            finalTarget = M210Manager.getInstance().getDroneBearing() - distance;
        }
        else {
            finalTarget = M210Manager.getInstance().getDroneBearing() + distance;
        }
        Logger.info("BEARING : schedule bearing task target angle is : " + finalTarget);
        st.scheduleBearingTask(finalTarget);
    }

}


