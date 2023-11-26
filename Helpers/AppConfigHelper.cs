using Crash_Launcher.DataStructure;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crash_Launcher.Helpers
{
    internal class AppConfigHelper
    {
        internal static async Task<Setting> getAppSetting()
        {
            string jsonContent;
            if (File.Exists(Path.Combine(SystemEnvironmentHelper.SystemAppDataPath, "CrashLauncher", "config.json")))
            {
                jsonContent = await File.ReadAllTextAsync(Path.Combine(SystemEnvironmentHelper.SystemAppDataPath, "CrashLauncher", "config.json"));
            }
            else
            {
                return new Setting();
            }
            return JsonSerializer.Deserialize<Setting>(jsonContent);
        }
    }
}
