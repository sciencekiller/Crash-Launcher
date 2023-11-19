using Crash_Launcher.DataStructure;
using System.Collections.Generic;
namespace Crash_Launcher.DataStructure
{
    internal class AppConfig
    {
        public static Enums.ProfileServers FastestProfileServer { get; set; }
        public static List<LanguageInfo> Languages { get; set; } = null;
    }
}
