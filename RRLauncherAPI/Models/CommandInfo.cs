using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRLauncherAPI.Models
{
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string name)
        {
            Name = name;
        }

        public String Name { get; set; }
    }

    public class CommandInfo
    {
        public CommandAttribute Info { get; set; }
        public Type Type { get; set; }
        public override string ToString() => Info.Name;
    }
}
