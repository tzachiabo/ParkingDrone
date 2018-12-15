package SharedClasses;

import android.os.AsyncTask;
import android.os.Looper;

import java.net.HttpURLConnection;
import java.net.URL;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class Logger {

    private static Logger instance;
    private ExecutorService LoggerExecutor;

    private Logger() {
        LoggerExecutor = new ThreadPoolExecutor(10, 10, 0L,
                TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
    }

    private static Logger getInstance(){
        if(instance==null){
            instance = new Logger();
        }
        return instance;
    }

    private static void innerLog(final String toLog) {

        Logger.getInstance().LoggerExecutor.submit(new Runnable() {
            @Override
            public void run() {
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
                    Logger.error("failed to write log : " + toLog);

                }
            }
        });
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