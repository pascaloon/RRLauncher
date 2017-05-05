using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RRLauncher.Services
{
    public class ConfigLoader
    {
        public Config Config { get; set; }

        public ConfigLoader()
        {
            Config = null;
        }

        public void Load()
        {
            if (Config != null)
                return;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
            using (var stream = new FileStream("./Resources/config.xml", FileMode.Open))
            {
                Config = (Config) xmlSerializer.Deserialize(stream);
            }
        }
    }

    public class Config
    {
        public String PluginsPath { get; set; }
    }
}
