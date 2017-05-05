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
        public List<CommandInfo> Commands { get; set; }
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
                    .Select(t => new CommandInfo()
                    {
                        Info = (CommandAttribute) t.GetCustomAttribute(typeof(CommandAttribute)),
                        Type = t
                    }).ToList();
            }

        public List<CommandInfo> FindCommands(String name)
        {
            if (Commands == null || String.IsNullOrWhiteSpace(name))
                return new List<CommandInfo>();
            return Commands.Where(c => c.Info.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public void ExecuteCommand(CommandInfo commandInfo)
        {
            Command instance = (Command) Activator.CreateInstance(commandInfo.Type);
            instance.Execute();
        }
    }
}
