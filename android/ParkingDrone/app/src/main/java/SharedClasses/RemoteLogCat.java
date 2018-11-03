package SharedClasses;

import android.os.AsyncTask;
import android.util.Log;
import java.io.BufferedInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

public class RemoteLogCat extends AsyncTask<String, String, String> {

    private String apikey="5bd57e77889d6";
    private static RemoteLogCat instance = null;

    public static RemoteLogCat getInstance()
    {
        if(instance == null)
        {
            instance = new RemoteLogCat();
        }
        return instance;
    }

    private RemoteLogCat(){
       // this.apikey = apikey;
    }

    public RemoteLogCat(String apikey){
        this.apikey = apikey;
    }

    public void log(String channel,String message)
    {
        try {
            this.execute("http://www.remotelogcat.com/log.php?apikey=" + apikey +
                    "&channel=" + URLEncoder.encode(channel, "utf-8") +
                    "&log=" + URLEncoder.encode(message, "utf-8"));
            Log.i(channel, message);
        }
        catch(UnsupportedEncodingException ex){

        }
    }

    public void debug(String message)
    {
        try {
            this.execute("http://www.remotelogcat.com/log.php?apikey=" + apikey +
                    "&channel=" + URLEncoder.encode("debug", "utf-8") +
                    "&log=" + URLEncoder.encode(message, "utf-8"));
            Log.i("debug", message);
        }
        catch(UnsupportedEncodingException ex){

        }
    }
    public void info(String message)
    {
        try {
            this.execute("http://www.remotelogcat.com/log.php?apikey=" + apikey +
                    "&channel=" + URLEncoder.encode("info", "utf-8") +
                    "&log=" + URLEncoder.encode(message, "utf-8"));
            Log.i("info", message);
        }
        catch(UnsupportedEncodingException ex){

        }
    }
    public void error(String message)
    {
        try {
            this.execute("http://www.remotelogcat.com/log.php?apikey=" + apikey +
                    "&channel=" + URLEncoder.encode("error", "utf-8") +
                    "&log=" + URLEncoder.encode(message, "utf-8"));
            Log.i("error", message);
        }
        catch(UnsupportedEncodingException ex){

        }
    }
    public void fatal(String message)
    {
        try {
            this.execute("http://www.remotelogcat.com/log.php?apikey=" + apikey +
                    "&channel=" + URLEncoder.encode("fatal", "utf-8") +
                    "&log=" + URLEncoder.encode(message, "utf-8"));
            Log.i("fatal", message);
        }
        catch(UnsupportedEncodingException ex){

        }
    }
    public void warn(String message)
    {
        try {
            this.execute("http://www.remotelogcat.com/log.php?apikey=" + apikey +
                    "&channel=" + URLEncoder.encode("warn", "utf-8") +
                    "&log=" + URLEncoder.encode(message, "utf-8"));
            Log.i("warn", message);
        }
        catch(UnsupportedEncodingException ex){

        }
    }

    public void log(String channel,String message,String apikey)
    {
        this.apikey = apikey;
        this.log(channel, message);
    }

    @Override
    protected String doInBackground(String... uri) {
        String responseString = "";
        try {
            URL url = new URL(uri[0]);
            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();
            InputStream in = new BufferedInputStream(urlConnection.getInputStream());
            responseString = new String(readFully(in), "utf-8");
        }
        catch(IOException ex)
        {
            responseString = ex.toString();
        }
        return responseString;
    }

    private byte[] readFully(InputStream inputStream)
            throws IOException {
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        byte[] buffer = new byte[1024];
        int length = 0;
        while ((length = inputStream.read(buffer)) != -1) {
            baos.write(buffer, 0, length);
        }
        return baos.toByteArray();
    }

    @Override
    protected void onPostExecute(String result) {
        super.onPostExecute(result);
    }
}