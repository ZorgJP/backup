using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace backup
{
    public static class Logger
    {
        private static LoggerSettings logger = new LoggerSettings();

        public static void SetInstance(LoggerSettings log)
        {
            logger = log;
        }

        public static LoggerSettings GetInstance()
        {
            return logger ?? new LoggerSettings();
        }

        public static void Debug(string message) => logger.Debug(message);
        public static void Info(string message) => logger.Info(message);
        public static void Error(string message) => logger.Error(message);
    }
}
