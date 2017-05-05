using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRLauncherAPI.Models
{

    public abstract class Command
    {
        public string Name { get; private set; }

        protected Command(string name)
        {
            Name = name;
        }

        public abstract void Execute();

        public override string ToString() => Name;
    }
}
