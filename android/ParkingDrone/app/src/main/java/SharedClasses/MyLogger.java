package SharedClasses;


import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.logging.*;

public class MyLogger {
    private final static Logger LOGGER =
            Logger.getLogger("Logger.My_Logger");
    private static MyLogger instance = null;

    private MyLogger()
    {

    }

    public static MyLogger getInstance()
    {
        if (instance == null)
            instance = new MyLogger();
        return instance;
    }

    public void debug(String message)
    {
        LOGGER.fine(message);
    }
    public void info(String message)
    {
        LOGGER.info(message);

    }
    public void error(String message)
    {
        LOGGER.severe(message);

    }
    public void fatal(String message)
    {
        LOGGER.severe(message);

    }
    public void warn(String message)
    {
        LOGGER.warning(message);

    }
}
