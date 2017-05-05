using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRLauncherAPI.Models;

namespace RRLauncherPlugin.Commands
{
    [Command]
    public class StartProcess : Command
    {
        public StartProcess() : base("Start Process")
        {
        }

        public override void Execute()
        {

        }

        public override List<Command> GetSubCommands(string input)
        {
            return new List<Command>()
            {
                new StartProcessExec(input)
            };
        }
    }

    [Command(Dynamic = true)]
    public class StartProcessExec : Command
    {
        private readonly string _programName;

        public StartProcessExec(string programName) : base("Run")
        {
            _programName = programName;
        }

        public override void Execute()
        {
            Process.Start(_programName);
        }

        public override List<Command> GetSubCommands(string input)
        {
            return new List<Command>()
            {
                new StartProcessExecWithArgs(_programName, input)
            };
        }
    }

    [Command(Dynamic = true)]
    public class StartProcessExecWithArgs : Command
    {
        private readonly string _programName;
        private readonly string _args;

        public StartProcessExecWithArgs(string programName, string args) : base("Run with args")
        {
            _programName = programName;
            _args = args;
        }

        public override void Execute()
        {
            Process.Start(_programName, _args);

        }
    }

}
