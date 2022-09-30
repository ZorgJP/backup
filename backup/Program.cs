using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using Ninject;

namespace backup
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ConfigereDIContainer();
            Logger.SetInstance(container.Get<LoggerSettings>());

            Logger.Debug("program started");
            Logger.Debug("default logger loaded");
            var backuper = container.Get<BackUper>();
            
            Logger.Info("backupper load success");
            EmulateFileBusiness("c:/1/11.txt");
            backuper.BackUp();
            backuper.SerializeSettings();
            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static StandardKernel ConfigereDIContainer()
        {
            var container = new StandardKernel();
            container.Bind<ISettingsReader>().To<JSonSettingsReader>()
                .WithConstructorArgument<string>("settings.json");            
            container.Bind<ErrorLogLevel>().ToConstant(ErrorLogLevel.Debug);
            container.Bind<TextWriter>().ToConstant(Console.Out);
            container.Bind<Settings>().ToMethod(c => Settings.Create(container.Get<ISettingsReader>()));
            container.Bind<LoggerSettings>().ToSelf();
            container.Bind<BackUper>().ToSelf(); 
            return container;
        }

        public static void EmulateFileBusiness(string fileName)
        {
            Logger.Debug($" start opening {fileName} to emulate business error");
            try
            {
                File.Open(fileName, FileMode.Open);
                Logger.Debug($"{fileName} opened");
            }
            catch (Exception e)
            {
                Logger.Error($"file business emulation failed with message: {e.Message}");
            }
        }
    }
}