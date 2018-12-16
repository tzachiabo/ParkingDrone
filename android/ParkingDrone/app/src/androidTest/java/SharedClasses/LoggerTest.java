package SharedClasses;

import org.junit.Test;

public class LoggerTest {

    @Test
    public void loggerOverload(){
        for(int i = 0 ; i<100000 ; i++){
            Logger.error("message number " + i);
        }
        Logger.join();
    }

}
