using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRLauncherAPI.Models;

namespace RRLauncherPlugin.Commands
{
    [Command("Calculator")]
    public class Calculator : Command
    {
        public Calculator() : base("Calculator")
        {
        }

        public override void Execute()
        {
            Process.Start("calc.exe");
        }
    }
}
