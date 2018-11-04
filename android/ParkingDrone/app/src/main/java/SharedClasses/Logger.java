package SharedClasses;

import android.os.AsyncTask;
import java.net.HttpURLConnection;
import java.net.URL;
public class Logger {

    private Logger() {
    }

    private static void innerLog(final String toLog) {
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                String url = "https://floating-fjord-95063.herokuapp.com/log/" + toLog;
                url = url.replaceAll(" ", "%20");
                try {
                    URL obj = new URL(url);
                    HttpURLConnection con = (HttpURLConnection) obj.openConnection();

                    // optional default is GET
                    con.setRequestMethod("GET");
                    String USER_AGENT = "Mozilla/5.0";
                    //add request header
                    con.setRequestProperty("User-Agent", USER_AGENT);
                    con.getResponseCode();
                    con.disconnect();
                } catch (Exception e) {
                    e.printStackTrace();
                }

                return "";
            }
        }.execute();
    }

    public static void debug(String message) {
        innerLog("debug : " + message);
    }

    public static void info(String message) {
        innerLog("info : " + message);
    }

    public static void error(String message) {
        innerLog("error : " + message);
    }

    public static void fatal(String message) {
        innerLog("fatal : " + message);
    }

    public static void warn(String message) {
        innerLog("warn : " + message);
    }
}