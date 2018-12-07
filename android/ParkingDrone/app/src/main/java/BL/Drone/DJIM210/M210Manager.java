package BL.Drone.DJIM210;

import BL.Drone.IDrone;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class M210Manager implements IDrone{

    Aircraft m_aircraft;
    ControllerManager m_controller;

    private static M210Manager instance;

    private M210Manager(){
        m_aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(m_aircraft != null,
                          "Aircraft is null when constracting M210Manager");

        m_controller = new ControllerManager(m_aircraft);
    }

    public static M210Manager getInstance(){
        if (instance == null){
            instance = new M210Manager();
        }
        return instance;
    }

    @Override
    public void takeOff(final Promise p) {
        m_controller.takeOff(p);
    }
}
