using System;
using System.Collections.Generic;
using System.IO;

namespace Crash_Launcher.Helpers
{
    internal class SystemEnvironmentHelper
    {
        internal static string SystemAppDataPath=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static void createAppDataDirectory()
        {
            string[] _paths = { "CrashLauncher", "CrashLauncher/Strings" };
            List<string> paths= new List<string>(_paths);
            foreach(string _path in paths)
            {
                string path=Path.Combine(SystemAppDataPath,_path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}
