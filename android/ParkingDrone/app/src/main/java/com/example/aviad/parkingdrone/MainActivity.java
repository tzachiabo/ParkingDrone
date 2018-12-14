package com.example.aviad.parkingdrone;

import android.graphics.Bitmap;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;


import java.nio.IntBuffer;

import javax.microedition.khronos.opengles.GL10;

import BL.BLManager;
import dji.ux.widget.FPVWidget;

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

    public static Bitmap SavePixels(int x, int y, int w, int h, GL10 gl)
    {
        int b[]=new int[w*(y+h)];
        int bt[]=new int[w*h];
        IntBuffer ib= IntBuffer.wrap(b);
        ib.position(0);
        gl.glReadPixels(x, 0, w, y+h, GL10.GL_RGBA, GL10.GL_UNSIGNED_BYTE, ib);

        for(int i=0, k=0; i<h; i++, k++)
        {
            //remember, that OpenGL bitmap is incompatible with Android bitmap
            //and so, some correction need.
            for(int j=0; j<w; j++)
            {
                int pix=b[i*w+j];
                int pb=(pix>>16)&0xff;
                int pr=(pix<<16)&0xffff0000;
                int pix1=(pix&0xff00ff00) | pr | pb;
                bt[(h-k-1)*w+j]=pix1;
            }
        }

        Bitmap sb = Bitmap.createBitmap(bt, w, h, Bitmap.Config.ARGB_8888);
        return sb;
    }
}
