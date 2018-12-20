package com.example.aviad.parkingdrone;

import android.graphics.Bitmap;
import android.graphics.SurfaceTexture;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.TextureView;
import android.view.View;


import java.nio.IntBuffer;

import javax.microedition.khronos.opengles.GL10;

import BL.BLManager;
import SharedClasses.Logger;
import dji.ux.widget.FPVWidget;

public class MainActivity extends AppCompatActivity {

    private BLManager bl_manager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        getSupportActionBar().hide();

        Thread.setDefaultUncaughtExceptionHandler(new Thread.UncaughtExceptionHandler() {
            @Override
            public void uncaughtException(Thread paramThread, Throwable paramThrowable) {
                Logger.fatal("UI exception thrown");
            }
        });

        bl_manager = BLManager.getInstance();

    }
    public void stopProgram(View v){

    }


}
