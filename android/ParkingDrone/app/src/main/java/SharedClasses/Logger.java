package SharedClasses;

import java.net.HttpURLConnection;
import java.net.URL;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

public class Logger {

    private static Logger instance;
    private ExecutorService LoggerExecutor;
    private boolean isRunning;
    ConcurrentLinkedQueue<String> logsToSend;

    private Logger() {
        isRunning = false;
        logsToSend = new ConcurrentLinkedQueue<String>();
        LoggerExecutor =  Executors.newSingleThreadExecutor();
        LoggerExecutor.submit(new Runnable() {
            @Override
            public void run() {
                while(true){
                    String message_to_send = "INFO : num of logs in QUEUE " + logsToSend.size() ;
                    for (int i = 0; i< Config.NUM_OF_LOG_IN_BUCKET && !logsToSend.isEmpty(); i++){
                        if (!message_to_send.equals("")){
                            message_to_send += ",";
                        }
                        message_to_send += logsToSend.remove();
                    }
                    sendGetRequest(message_to_send);
                }
            }
        });
    }

    private void insertToQueue(String message){
        logsToSend.add(message);
    }

    private void sendGetRequest(String message){
        String url = "https://floating-fjord-95063.herokuapp.com/log/" + message;
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
        }
    }

    private static Logger getInstance(){
        if(instance==null){
            instance = new Logger();
        }
        return instance;
    }

    public static void join() {
        try {
            while(!instance.logsToSend.isEmpty()) {
                Thread.sleep(500);
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public static void debug(String message) {
        getInstance().insertToQueue("debug : " + message);
    }

    public static void info(String message) {
        getInstance().insertToQueue("info : " + message);
    }

    public static void error(String message) {
        getInstance().insertToQueue("error : " + message);
    }

    public static void fatal(String message) {
        getInstance().insertToQueue("fatal : " + message);
    }

    public static void warn(String message) {
        getInstance().insertToQueue("warn : " + message);
    }
}