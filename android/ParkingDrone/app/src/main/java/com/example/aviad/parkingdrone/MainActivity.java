package com.example.aviad.parkingdrone;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;


import BL.BLManager;
import SharedClasses.RemoteLogCat;

public class MainActivity extends AppCompatActivity {

    private BLManager bl_manager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        for (int i=0;i<100;i++)
            RemoteLogCat.error("try "+ i);
         bl_manager = BLManager.getInstance();

    }
    public void stopProgram(View v){

    }
}
