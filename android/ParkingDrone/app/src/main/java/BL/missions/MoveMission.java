package BL.missions;
import java.util.Date;
import java.util.Timer;
import java.util.TimerTask;

import BL.Config;
import BL.SuperTimer;
import SharedClasses.Direction;
import SharedClasses.RemoteLogCat;
import dji.common.error.DJIError;
import dji.common.flightcontroller.virtualstick.FlightControlData;
import dji.common.flightcontroller.virtualstick.FlightCoordinateSystem;
import dji.common.flightcontroller.virtualstick.RollPitchControlMode;
import dji.common.flightcontroller.virtualstick.VerticalControlMode;
import dji.common.flightcontroller.virtualstick.YawControlMode;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class MoveMission extends Mission {

    private Direction direction;
    private double distance;
    RemoteLogCat logger=RemoteLogCat.getInstance();
    public MoveMission(int index, Direction direction, double distance){
        super("move", index);
        this.direction=direction;
        this.distance=distance;
    }

    public void real_start() {
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();

        aircraft.getFlightController().setRollPitchControlMode(RollPitchControlMode.VELOCITY);
        aircraft.getFlightController().setYawControlMode(YawControlMode.ANGULAR_VELOCITY);
        aircraft.getFlightController().setVerticalControlMode(VerticalControlMode.VELOCITY);
        aircraft.getFlightController().setRollPitchCoordinateSystem(FlightCoordinateSystem.BODY);
        long totalTime = ((long)distance/(long)Config.BASE_SPEED) * 1000;
        logger.debug("start-move-mission "+"distance is "+distance+" totalTime is "+totalTime);
        SuperTimer st = new SuperTimer(new TimerTask() {
            @Override
            public void run() {
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
                        logger.debug("UP mission");
                        break;
                    case down:
                        fcd = new FlightControlData(0,0,0,-Config.BASE_SPEED);
                        break;
                    default:
                        logger.error("Couldnt parse move direction");
                        break;
                }

                logger.debug("start-move-mission with thortle " + fcd.getVerticalThrottle());
                aircraft.getFlightController().sendVirtualStickFlightControlData(fcd, new CommonCallbacks.CompletionCallback() {
                    @Override
                    public void onResult(DJIError djiError) {
                        if(djiError != null)
                            logger.debug("after move djierror is " + djiError.toString());
                        else
                        {
                            logger.error("after move djierror is null");
                        }
                        //aircraft.getFlightController().setVirtualStickModeEnabled(false, null);
                    }
                });


            }
        },this,Config.MOVMENT_BASE_INTERVAL,totalTime);
        st.schedule();
    }

    @Override
    public void start() {
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        aircraft.getFlightController().setVirtualStickModeEnabled(true, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                real_start();
            }
        });
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }
}
