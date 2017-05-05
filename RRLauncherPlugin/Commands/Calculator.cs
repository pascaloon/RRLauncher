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
    public class Calculator : Command
    {
        public Calculator() : base("Calculator")
        {
        }

        public override void Execute()
        {
            Process.Start("calc.exe");
        }

        public override List<Command> GetSubCommands(string input)
        {
            return new List<Command>()
            {
                new CalculatorCopy(input)
            };
        }
    }

    [Command(Dynamic = true)]
    public class CalculatorCopy : Command
    {
        public string Input { get; set; }

        public CalculatorCopy(string input) : base("Calculator Copy")
        {
            Input = input;
        }

        public override void Execute()
        {
            Debug.WriteLine($"{Input} copied");
        }
    }
}
