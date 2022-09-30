using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace backup
{
    [Serializable]
    public class LoggerSettings
    {
        public ErrorLogLevel LoggerLevel { get; set; }
        public string OutFileName { get; set; }
        private TextWriter DefaultWriter { get; set; }

        public LoggerSettings() { }

        public LoggerSettings(TextWriter writer, ErrorLogLevel level)
        {
            DefaultWriter = writer;
            LoggerLevel = level;
        }

        //public LoggerSettings(string fileName = null, ErrorLogLevel level = ErrorLogLevel.Debug, TextWriter writer = null)
        //{
        //    LoggerLevel = level;
        //    OutFileName = fileName;
        //    DefaultWriter = writer ?? Console.Out;
        //}

        public void Debug(string message) => WriteIfPossible(ErrorLogLevel.Debug, message);
        public void Info(string message) => WriteIfPossible(ErrorLogLevel.Info, message);
        public void Error(string message) => WriteIfPossible(ErrorLogLevel.Error, message);

        private void WriteIfPossible(ErrorLogLevel level, string message)
        {
            if (LoggerLevel <= level)
            {
                using (var writer = OutFileName == null ?
                     DefaultWriter : new StreamWriter(File.Open(OutFileName, FileMode.Append)))
                        writer.WriteLine(FormatMessage(message, level));
            }
        }

        private string FormatMessage(string message, ErrorLogLevel level)
        {
            return string.Format($"{DateTime.Now:G} | {level} | {message}");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Loggersettings object:"+Environment.NewLine);
            sb.Append($"LoggerLevel: {LoggerLevel}" + Environment.NewLine);
            sb.Append($"out File: {OutFileName ?? string.Empty}" + Environment.NewLine);
            return sb.ToString();
        }
    }
}