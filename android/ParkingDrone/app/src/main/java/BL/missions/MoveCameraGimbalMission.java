package BL.missions;

import java.util.List;

import SharedClasses.AssertionViolation;
import SharedClasses.Config;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.gimbal.GimbalMode;
import dji.common.gimbal.Rotation;
import dji.common.gimbal.RotationMode;
import dji.common.util.CommonCallbacks;
import dji.sdk.gimbal.Gimbal;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class MoveCameraGimbalMission extends Mission {

    private double roll;
    private double pitch;
    private double yaw;
    private String direction;
    private String gimbal_movement_type;

    public MoveCameraGimbalMission(int index, String direction, String gimbal_movement_type, double roll, double pitch, double yaw){
        super("moveGimbal", index);
        this.direction=direction;
        this.gimbal_movement_type = gimbal_movement_type;
        this.roll=roll;
        this.pitch=pitch;
        this.yaw=yaw;
    }
    @Override
    public void start() {
        Logger.info("start move gimbal mission");
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "when tring to move gimbal got null aircraft");

        Gimbal gimbal_to_move = getGimbal(aircraft);

        Logger.debug("send gimbal mode free");
        gimbal_to_move.setMode(GimbalMode.FREE, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError != null){
                    Logger.debug("allow rotate gimbal result : " + djiError.toString());
                    Assertions.verify(false, "failed to set gimbal mode to free");
                }

                Logger.debug("start rotate Gimbal");
                List<Gimbal> gimbals= aircraft.getGimbals();
                Rotation.Builder builder = new Rotation.Builder();
                builder.yaw((float)yaw).pitch((float)pitch).roll((float)roll);

                Logger.info("gimbal movement type : " + gimbal_movement_type);
                if(gimbal_movement_type.equals("absolute")) {
                    Logger.info("Move gimbal absolute");
                    builder.mode(RotationMode.ABSOLUTE_ANGLE);
                }
                else if(gimbal_movement_type.equals("relative"))
                {
                    Logger.info("Move gimbal relative");
                    builder.mode(RotationMode.RELATIVE_ANGLE);
                }

                builder.time(Config.TIME_OF_GIMBAL_MOVE);

                final Rotation rotation = builder.build();
                Gimbal gimbal_to_move = getGimbal(aircraft);

                gimbal_to_move.rotate(rotation, new CommonCallbacks.CompletionCallback() {
                    @Override
                    public void onResult(DJIError djiError) {
                        try{
                            if(djiError != null){
                                Logger.error("Move gimbal result in djiError : "+ djiError.toString());
                                Assertions.verify(false, "failed to move gimbal");
                            }
                            else{
                                Logger.debug("move gimbal has finished");
                                try {
                                    Thread.sleep(Config.TIME_OF_GIMBAL_MOVE * 1000);
                                } catch (InterruptedException e) {
                                }
                                onResult.onResult(djiError);
                            }
                        }
                        catch (AssertionViolation e){

                        }
                    }
                });
            }
        });


    }

    @Override
    public void stop() {
        Logger.info("move camera got stop request - do nothing");
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " Done";
    }

    private Gimbal getGimbal(Aircraft aircraft){
        Gimbal gimbal_to_move = null;

        List<Gimbal> gimbals= aircraft.getGimbals();

        for(Gimbal gimbal : gimbals){
            if(gimbal.getIndex() == 0 && direction.equals("left"))// left gimbal
            {
                gimbal_to_move = gimbal;
            }
            else if(gimbal.getIndex() == 1 && direction.equals("right"))// right gimbal
            {
                gimbal_to_move = gimbal;
            }
        }

        Assertions.verify(gimbal_to_move != null, "could not find a gimbal to move");

        return gimbal_to_move;
    }
}
