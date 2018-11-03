package BL;

import android.arch.core.util.Function;
import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

import SharedClasses.RemoteLogCat;
import dji.common.error.DJIError;
import dji.common.error.DJISDKError;
import dji.common.flightcontroller.virtualstick.FlightCoordinateSystem;
import dji.common.flightcontroller.virtualstick.RollPitchControlMode;
import dji.common.flightcontroller.virtualstick.VerticalControlMode;
import dji.common.flightcontroller.virtualstick.YawControlMode;
import dji.common.util.CommonCallbacks;
import dji.sdk.base.BaseComponent;
import dji.sdk.base.BaseProduct;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class BLManager {
    private static BLManager instance = null;
    SocketManager socket_manager;


    private BLManager()
    {
        RemoteLogCat.debug("initiate BL");
        socket_manager = SocketManager.getInstance();
    }

    public static BLManager getInstance(){
        if (instance == null)
        {
            instance = new BLManager();
        }
        return instance;
    }



}
