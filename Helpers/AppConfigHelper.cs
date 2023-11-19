using Crash_Launcher.DataStructure;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crash_Launcher.Helpers
{
    internal class AppConfigHelper
    {
        internal static async Task getAppSetting()
        {
            var jsonContent = await File.ReadAllTextAsync(Path.Combine(SystemEnvironmentHelper.SystemAppDataPath, "CrashLauncher", "config.json"));
            AppConfig.setting = JsonSerializer.Deserialize<Setting>(jsonContent);
        }
    }
}
