using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace DroneServer.SharedClasses
{
    enum module
    {
        StatusMission,
        MainMission
    }

    public class Logger : BaseObservable
    {
        private static ILog Log;
        private static Logger instance = null;
        private static List<string> llogs=new List<string>();

        private Logger():base()
        {
            Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        
        public static Logger getInstance()
        {
            if (instance == null)
                instance = new Logger();
            return instance;
        }

        public override object getData()
        {
            return llogs;
        }

        public void clearData()
        {
            llogs.Clear();
            notifyAll();
        }

        public void debug(string message)
        {
            //Log.Debug(message);
            //llogs.Insert(0, message);
            //notifyAll();
        }
        public void info(string message)
        {
            Log.Info(message);
            llogs.Insert(0, message);
            notifyAll();
        }
        public void error(string message)
        {
            Log.Error(message);
            llogs.Insert(0, message);
            notifyAll();
        }
        public void fatal(string message)
        {
            Log.Fatal(message);
            llogs.Insert(0, message);
            notifyAll();
        }
        public void warn(string message)
        {
            Log.Warn(message);
            llogs.Insert(0, message);
            notifyAll();
        }
    }
}
