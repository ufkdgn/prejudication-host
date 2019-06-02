using log4net;
using log4net.Appender;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LawPortal.Types.Common
{
    public class LogManager
    {
        private static ILog logger;
        public static ILog Current
        {
            get
            {
                if (logger == null)
                {
                    // todo
                }
                return logger;
            }
        }

        public static void Log(string message)
        {
            Current.Debug(message);
        }

        public static void Log(Exception message)
        {
            Current.Debug(message);
        }

        public static void Log(string controller, string message)
        {
            Log(string.Format("{0} : {1}", controller, message));
        }

        public static void Log(string controller, Exception message)
        {
            Log(string.Format("{0} :", controller), message.Message);
        }

        public static void Log(string userName, string controller, string message)
        {
            Log(string.Format("{0} > {1} : {2}", userName, controller, message));
        }

        public static void Log(string userName, string controller, Exception message)
        {
            Log(string.Format("{0} > {1} :", userName, controller), message);
        }
    }
}
