using System;

namespace Crash_Launcher.Helpers
{
    internal class SystemEnvironmentHelper
    {
        internal static string SystemAppDataPath=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}
