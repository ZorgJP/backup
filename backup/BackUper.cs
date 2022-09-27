using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backup
{
    public class BackUper
    {
        public readonly Settings Settings;

        public BackUper(ISettingsReader settingsReader)
        {
            Settings = Settings.Create(settingsReader);
            Logger.Debug("settings readed sucessful");
            Logger.Debug(Settings.ToString());
        }

        public void SerializeSettings() => Settings.SerializeToFile();

        public void BackUp()
        {
            Logger.Debug("backup method was invoked");
            Logger.Info("starting backup");
            var dateDirectory = Settings.Destination.CreateSubdirectory($"{DateTime.Now:dd.MM.yyyy_hh.mm}");
            Logger.Debug($"created directory {dateDirectory.FullName}");
            foreach (DirectoryInfo sourceFolder in Settings.Sources)
            {
                Logger.Debug($"start to backup directory {sourceFolder.FullName}");
                var backupDirectory = dateDirectory.CreateSubdirectory(sourceFolder.Name);
                Logger.Debug($"created directory {backupDirectory.FullName}");
                var files = sourceFolder.GetFiles("*");
                foreach (FileInfo file in files)
                {
                    try
                    {
                        file.CopyTo($"{backupDirectory.FullName}/{file.Name}", true);
                        Logger.Debug($"file {file.FullName} was backuped");
                    }
                    catch (Exception e){ 
                        Logger.Error($"backup file {file.FullName} failed with error: {e.Message}"); 
                    }
                }
                Logger.Debug($"directory {sourceFolder.FullName} backuped");
            }
            Logger.Info("backup done");
        }
    }
}
