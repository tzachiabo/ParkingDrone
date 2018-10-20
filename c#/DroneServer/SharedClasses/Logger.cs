using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace DroneServer.SharedClasses
{
    class Logger : BaseObservable
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Logger instance = null;
        private static List<string> llogs=new List<string>();

        private Logger():base()
        {
            
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

        public void debug(string message)
        {
            Log.Debug(message);
            llogs.Add(message);
            notifyAll();
        }
        public void info(string message)
        {
            Log.Info(message);
            llogs.Add(message);
            notifyAll();
        }
        public void error(string message)
        {
            Log.Error(message);
            llogs.Add(message);
            notifyAll();
        }
        public void fatal(string message)
        {
            Log.Fatal(message);
            llogs.Add(message);
            notifyAll();
        }
        public void warn(string message)
        {
            Log.Warn(message);
            llogs.Add(message);
            notifyAll();
        }
    }
}
