package BL;

import android.util.Log;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.fluent.Request;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.junit.Test;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import SharedClasses.Config;

import static org.junit.Assert.*;

public class BLManagerTest {
    @Test
    public void go(){
        String uri = "http://" + Config.DST_ADDRESS + ":" +Config.DST_PIC_PORT;
        Request request = Request.Get(uri);
        try
        {
            HttpResponse response = request.execute().returnResponse();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }
}