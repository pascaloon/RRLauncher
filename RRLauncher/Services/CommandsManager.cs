using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RRLauncherAPI.Models;

namespace RRLauncher.Services
{
    public class CommandsManager
    {
        private readonly ConfigLoader _config;
        public List<Command> Commands { get; set; }
        public CommandsManager(ConfigLoader config)
        {
            _config = config;
        }

        public void LoadCommands()
        {
            _config.Load();
            String pluginsPath = _config.Config.PluginsPath;
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            

            Commands =
                Directory.GetFiles(Path.Combine(exePath, pluginsPath))
                    .Where(f => f.EndsWith(".dll"))
                    .Select(Assembly.LoadFile)
                    .Select(assembly => assembly.GetExportedTypes())
                    .SelectMany(x => x)
                    .Where(t => t.IsSubclassOf(typeof(Command)))
                    .Select(t => new
                    {
                        Info = (CommandAttribute) t.GetCustomAttribute(typeof(CommandAttribute)),
                        Type = t
                    })
                    .Where(info => !info.Info.Dynamic)
                    .Select(c => c.Type)
                    .Select(InstanciateCommand)
                    .ToList();
        }

        public List<Command> FindCommands(String name)
        {
            if (Commands == null || String.IsNullOrWhiteSpace(name))
                return new List<Command>();
            return Commands.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public Command InstanciateCommand(Type commandType) => (Command)Activator.CreateInstance(commandType);

        public List<Command> FindSubCommands(Command instance, String input)
        {
            if (instance == null)
                return new List<Command>();
            return instance.GetSubCommands(input);
        }


        public void ExecuteCommand(Command command) => command?.Execute();


    }
}
