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
    public class JSonSettingsReader : ISettingsReader
    {
        private string fileName;

        public JSonSettingsReader(string fileName)
        {
            this.fileName = fileName;
            Logger.Debug("Json reader created");
        }

        public Settings ReadSettings()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonDirectoryInfoConverter() }
                };
                var settings = JsonSerializer
                    .Deserialize<Settings>(File.ReadAllText(fileName), options);
                Logger.Debug("settings readed from json");
                return settings;
            }
            catch (Exception e)
            {
                Logger.Error($"json reader failed whith message: {e.Message}");
            }
            return null;
        }
    }
}
