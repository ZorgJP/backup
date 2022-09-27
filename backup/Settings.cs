using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backup
{
    [Serializable]
    public class Settings
    {
        public DirectoryInfo Destination { get; set; }
        public IEnumerable<DirectoryInfo> Sources{ get; set; }
  
        public LoggerSettings Log { 
            get { return Logger.GetInstance(); } 
            set { Logger.SetInstance(value); } 
        }
        
        public Settings() 
        {
           Logger.Debug("Settings object created");
        }

        public Settings(string destination, IEnumerable<string> source, LoggerSettings logger) : base()
        {
            Destination = new DirectoryInfo(destination);
            Sources = source.Select(s => new DirectoryInfo(s));
            Log = logger; 
            Logger.Debug("Settings object filled with data");
        }

        public static Settings Create(ISettingsReader reader)
        {
            return reader.ReadSettings();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Settings object:" + Environment.NewLine);
            sb.Append($"Destination : {Destination.FullName}"
                + Environment.NewLine);
            foreach (var source in Sources)
                sb.Append($"Source : {source.FullName}"
                    +Environment.NewLine);
            sb.Append(Log.ToString());
            return sb.ToString();
        }

        public void SerializeToFile()
        {
            Logger.Debug("settings serialization started");
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new DirectoryConverter() }
                };
                var jsonData = JsonSerializer.Serialize<Settings>(this, options);
                File.WriteAllText("settings.json", jsonData);
                Logger.Info("Data has been saved to file");
            }
            catch (Exception e)
            {  
                Logger.Error($"Serialization failed with message: {e.Message}"); 
            }
        }
    }
}