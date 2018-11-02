package SharedClasses;

public class Assertions {

    public static void verify(Boolean predicat, String message)
    {
        RemoteLogCat logger=new RemoteLogCat();
        if (!predicat)
        {
            logger.error("assertion failure with message : " + message);
            
            //System.Windows.Forms.Application.Exit();
        }

    }

}
