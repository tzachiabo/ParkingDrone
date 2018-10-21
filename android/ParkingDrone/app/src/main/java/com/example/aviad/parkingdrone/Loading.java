package com.example.aviad.parkingdrone;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.Toast;

import dji.common.error.DJIError;
import dji.common.error.DJISDKError;
import dji.sdk.base.BaseComponent;
import dji.sdk.base.BaseProduct;
import dji.sdk.sdkmanager.DJISDKManager;

public class Loading extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_loading);
        ProgressBar spinner;
        spinner = (ProgressBar)findViewById(R.id.progressBar1);
        DJISDKManager.getInstance().registerApp(getBaseContext(), new DJISDKManager.SDKManagerCallback() {

            @Override
            public void onRegister(DJIError djiError) {
                if (djiError == DJISDKError.REGISTRATION_SUCCESS){
                    startActivity(new Intent(Loading.this, MainActivity.class));
                }
                else
                {
                    Toast.makeText(getBaseContext(),"Registeration failed ",
                            Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onProductDisconnect() {

            }

            @Override
            public void onProductConnect(BaseProduct baseProduct) {

            }

            @Override
            public void onComponentChange(BaseProduct.ComponentKey componentKey, BaseComponent baseComponent, BaseComponent baseComponent1) {

            }
        });

    }

}
