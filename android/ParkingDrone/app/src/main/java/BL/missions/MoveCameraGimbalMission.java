package BL.missions;

import java.util.List;

import dji.common.error.DJIError;
import dji.common.gimbal.Rotation;
import dji.common.util.CommonCallbacks;
import dji.sdk.gimbal.Gimbal;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class MoveCameraGimbalMission extends Mission {

    private double roll;
    private double pitch;
    private double yaw;
    private String direction;

    public MoveCameraGimbalMission(int index, String direction, double roll, double pitch, double yaw){
        super("moveGimbal", index);
        this.direction=direction;
        this.roll=roll;
        this.pitch=pitch;
        this.yaw=yaw;
    }
    @Override
    public void start() {
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        List<Gimbal> gimbals= aircraft.getGimbals();
        Rotation.Builder builder = new Rotation.Builder();
        builder.yaw(5f);
        builder.pitch(5f);
        builder.roll(5F);
        Rotation rotation = builder.build();
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
        gimbal_to_move.rotate(rotation, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                onResult.onResult(djiError);
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
