package com.example.aviad.parkingdrone;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import BL.BLManager;

public class MainActivity extends AppCompatActivity {
    private BLManager bl_manager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        bl_manager = BLManager.getInstance();

    }
    public void stopProgram(View v){

    }
}
