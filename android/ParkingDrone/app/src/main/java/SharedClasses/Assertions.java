package SharedClasses;

public class Assertions {

    public static void verify(Boolean predicat, String message)
    {
        if (!predicat)
        {
            RemoteLogCat.error("assertion failure with message : " + message);
            
            //System.Windows.Forms.Application.Exit();
        }

    }

}
