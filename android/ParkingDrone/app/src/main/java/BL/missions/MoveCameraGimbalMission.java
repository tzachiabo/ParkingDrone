package BL.missions;

import java.util.List;

import SharedClasses.Assertions;
import SharedClasses.RemoteLogCat;
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
        Gimbal gimbal_to_move = null;
        RemoteLogCat.info("start move gimbal mission");
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();

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
        RemoteLogCat.debug("send gimbal mode free");
        gimbal_to_move.setMode(GimbalMode.FREE, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError != null)
                    RemoteLogCat.debug("allow rotate gimbal result : " + djiError.toString());
                else
                    RemoteLogCat.debug("allow rotate gimbal return djiError null");

                final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
                RemoteLogCat.debug("start rotate Gimbal");
                List<Gimbal> gimbals= aircraft.getGimbals();
                RemoteLogCat.debug("Gimbals were collected");
                Rotation.Builder builder = new Rotation.Builder();
                builder.yaw((float)yaw);
                RemoteLogCat.info("Move gimbal set yaw : "+ yaw);
                builder.pitch((float)pitch);
                RemoteLogCat.info("Move gimbal set pitch : "+ pitch);
                builder.roll((float)roll);
                if(gimbal_movement_type.equals("absolute")) {
                    RemoteLogCat.info("Move gimbal absolute");
                    builder.mode(RotationMode.ABSOLUTE_ANGLE);
                }
                else if(gimbal_movement_type.equals("relative"))
                {
                    RemoteLogCat.info("Move gimbal relative");
                    builder.mode(RotationMode.RELATIVE_ANGLE);
                }
                builder.time(2);
                RemoteLogCat.info("Move gimbal set roll : "+ roll);

                final Rotation rotation = builder.build();
                RemoteLogCat.debug("Rotate obj was created");
                Gimbal gimbal_to_move = null;
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
                RemoteLogCat.debug("Gimbal was choosed");

                Assertions.verify(gimbal_to_move != null, "Gimbal is null");
                gimbal_to_move.rotate(rotation, new CommonCallbacks.CompletionCallback() {
                    @Override
                    public void onResult(DJIError djiError) {
                        if(djiError != null)
                            RemoteLogCat.error("Move gimbal result in djiError : "+ djiError.toString());

                        onResult.onResult(djiError);
                    }
                });
            }
        });


    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " Done";
    }
}
