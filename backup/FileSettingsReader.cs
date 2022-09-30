using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace backup
{
    public class FileSettingsReader : ISettingsReader
    {
        private string fileName;

        public FileSettingsReader(string fileName)
        {
            this.fileName = fileName;
            Logger.Debug("TextFile reader created");
        }

        private LoggerSettings GetLogger()
        {
            try
            {
                var fName = File.ReadAllLines(fileName).First();
                var strErr = File.ReadAllLines(fileName).Skip(1).First();
                var logger = new LoggerSettings();// (fName, (ErrorLogLevel)Enum.Parse(typeof(ErrorLogLevel), strErr));
                Logger.Debug("Logger was created from file");
                return logger;
            }
            catch (Exception e)
            {
                Logger.Error($"creating logger from file failed with message: {e.Message}");
            }
            return null;
        }

        private string GetDestination()
        {
            try
            {
                var destination = File.ReadLines(fileName).Skip(2).First();
                Logger.Debug("Destination readed from text file");
                return destination;
            }
            catch (Exception e)
            {
                Logger.Error($"reading destination from text file failed with message: {e.Message}");
            }
            return null;
        }

        private IEnumerable<string> GetSource()
        {
            foreach (var line in File.ReadLines(fileName).Skip(3))
                yield return line;
        }

        public Settings ReadSettings()
        {
            return new Settings(GetDestination(), GetSource(), GetLogger());
        }
    }
}