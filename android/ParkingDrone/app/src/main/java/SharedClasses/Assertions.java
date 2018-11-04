package SharedClasses;

import java.io.IOException;

import BL.SocketManager;
import BL.TaskManager;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class Assertions {

    public static void verify(Boolean predicat, String message)
    {
        if (!predicat)
        {
            Logger.error("assertion failure with message : " + message);

            try {
                SocketManager.getInstance().close_socket();
            }
            catch (IOException e){
                Logger.fatal("Failed to close socket");
            }

            final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
            aircraft.getFlightController().setVirtualStickModeEnabled(false, new CommonCallbacks.CompletionCallback() {
                @Override
                public void onResult(DJIError djiError) {
                    if(djiError == null){
                        Logger.info("set virtual stick off");
                    }
                    else {
                        Logger.fatal("Failed to set virtual stick off");
                    }
                }
            });

            TaskManager.getInstance().stopAllTasks();
        }

    }

}
