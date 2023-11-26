using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash_Launcher.DataStructure
{
    internal class Setting
    {
        public string language { get; set; }
        public string dataPath { get; set; }
        public Enums.ProfileServers CDN {  get; set; }
        internal void writeToAppConfig()
        {
            AppConfig.Language = language;
            AppConfig.DataPath = dataPath;
            AppConfig.CDN = CDN;
        }
    }
}
