package com.example.aviad.parkingdrone;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import com.google.firebase.analytics.FirebaseAnalytics;


import BL.BLManager;
import SharedClasses.MyLogger;
import SharedClasses.RemoteLogCat;

public class MainActivity extends AppCompatActivity {

    private BLManager bl_manager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);





        MyLogger.getInstance().mFirebaseAnalytics=FirebaseAnalytics.getInstance(this);
        MyLogger.getInstance().debug("test");

         bl_manager = BLManager.getInstance();

    }
    public void stopProgram(View v){

    }
}
