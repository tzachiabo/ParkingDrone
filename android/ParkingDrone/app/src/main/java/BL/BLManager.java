package BL;

import android.arch.core.util.Function;
import android.content.Context;
import android.os.AsyncTask;

import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

import dji.common.error.DJIError;
import dji.common.error.DJISDKError;
import dji.common.util.CommonCallbacks;
import dji.sdk.base.BaseComponent;
import dji.sdk.base.BaseProduct;
import dji.sdk.sdkmanager.DJISDKManager;

public class BLManager {
    private static BLManager instance = null;
    DroneSingleton  drone;
    SocketManager socket_manager;


    private BLManager()
    {
        drone = DroneSingleton.getInstance();
        socket_manager = SocketManager.getInstance();
    }

    public static CompletableFuture<Boolean> init(Context context)
    {
        return null;
    }
    public static BLManager getInstance(){
        return instance;
    }


    private class Register extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {
            return "Download failed";
    }

    @Override
    protected void onPostExecute(String result) {

    }
}
}
