package SharedClasses;

import java.io.DataOutputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLConnection;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.util.HashMap;
import java.util.Map;
import java.util.StringJoiner;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class Logger {

    private static Logger instance;
    private ExecutorService LoggerExecutor;
    ConcurrentLinkedQueue<String> logsToSend;

    private Logger() {
        logsToSend = new ConcurrentLinkedQueue<String>();
        LoggerExecutor =  new ThreadPoolExecutor(1, 1, 0L,
                TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
        for (int i=0;i<1;i++) {
            LoggerExecutor.submit(new Runnable() {
                @Override
                public void run() {
                    while (true) {
                        String message_to_send;
                        synchronized (this) {
                            message_to_send = "";
                            for (int i = 0; i < Config.NUM_OF_LOG_IN_BUCKET && !logsToSend.isEmpty(); i++) {
                                if (!message_to_send.equals("")) {
                                    message_to_send += ",";
                                }
                                message_to_send += logsToSend.remove();
                            }
                        }
                        sendGetRequest(message_to_send);
                    }
                }
            });
        }
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

//    private void sendPost(String message) {
//        StringBuilder sbParams = new StringBuilder();
//        sbParams.append("logs").append("=")
//                .append(URLEncoder.encode(params.get(key), "UTF-8"));
//
//        try {
//            URL urlObj = new URL("https://floating-fjord-95063.herokuapp.com/log");
//            HttpURLConnection conn = (HttpURLConnection) urlObj.openConnection();
//            conn.setDoOutput(true);
//            conn.setRequestMethod("POST");
//            conn.setRequestProperty("Accept-Charset", "UTF-8");
//
//            conn.setReadTimeout(10000);
//            conn.setConnectTimeout(15000);
//
//            conn.connect();
//
//            String paramsString = sbParams.toString();
//
//            DataOutputStream wr = new DataOutputStream(conn.getOutputStream());
//            wr.writeBytes(paramsString);
//            wr.flush();
//            wr.close();
//
//        }
//        catch(Exception e){
//            sendGetRequest(e.toString());
//        }
//    }

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