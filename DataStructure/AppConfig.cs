using Crash_Launcher.DataStructure;
using System.Collections.Generic;
using Windows.Globalization;
namespace Crash_Launcher.DataStructure
{
    internal class AppConfig
    {
        public static Enums.ProfileServers FastestProfileServer { get; set; }
        public static List<LanguageInfo> Languages { get; set; } = null;
        public static string Language { get; set; }
        public static string DataPath { get; set; }
        public static Enums.ProfileServers CDN { get; set; }

        //Constants
        internal const int totalSet = 3;
        //Method
        internal static List<Enums.InitializeStep> checkSetting()
        {
            List<Enums.InitializeStep> steps = new List<Enums.InitializeStep>();
            if (Language == string.Empty)
            {
                steps.Add(Enums.InitializeStep.Language);
            }
            if (DataPath == string.Empty)
            {
                steps.Add(Enums.InitializeStep.Path);
            }
            if (CDN == Enums.ProfileServers.None)
            {
                steps.Add(Enums.InitializeStep.CDN);
            }
            return steps;
        }
    }
}
