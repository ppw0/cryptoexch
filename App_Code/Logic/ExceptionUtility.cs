using System;
using System.IO;
using System.Web;

namespace Cryptoexch.Logic
{
    public sealed class ExceptionUtility
    {
        private ExceptionUtility()
        {
        }

        public static void LogException(Exception exc, string source)
        {
            string logFile = "~/App_Data/ErrorLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.WriteLine("Inner Exception Type: " + exc.InnerException.GetType().ToString() + "\r\n" +
                             "Inner Exception Message: " + exc.InnerException.Message + "\r\n" +
                             "Inner Exception Source: " + exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                    sw.WriteLine("Inner Exception Stack Trace: " + exc.InnerException.StackTrace);
            }
            sw.WriteLine("Exception Type: " + exc.GetType().ToString() + "\r\n" +
                         "Exception: " + exc.Message + "\r\n" +
                         "Source: " + source);
            if (exc.StackTrace != null)
                sw.WriteLine("Stack trace: " + "\r\n" + exc.StackTrace + "\r\n");
            sw.Close();
        }
    }
}