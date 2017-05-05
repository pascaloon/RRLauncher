using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRLauncherAPI.Models
{
    public class CommandAttribute : Attribute
    {
        public CommandAttribute()
        {
        }

        public bool Dynamic { get; set; } = false;
    }

    public class CommandInfo
    {
        public CommandAttribute Info { get; set; }
        public Command Command { get; set; }
    }
}
