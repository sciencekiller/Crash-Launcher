using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Crash_Launcher.DataStructure;
namespace Crash_Launcher.Helpers
{
    class LanguageHelper
    {
        internal static string systemLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name;
        internal static string appLanguage=string.Empty;
        internal static string[] files = { "Initialize.resw" };
        private const string defaultLanguage = "en";
        internal static async Task getLocalizationInfo()
        {
            string inf = await ResourcesHelper.getStringFromResourcesFile("Resources.Localization.LocalizationInfo.json");
            AppConfig.Languages = JsonSerializer.Deserialize<List<LanguageInfo>>(inf);
        }
        internal static async Task checkLanguageFiles()
        {
            Trace.WriteLine(appLanguage);
            string path = Path.Combine(SystemEnvironmentHelper.SystemAppDataPath, "CrashLauncher", "Strings");
            //创建主文件夹
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //获取应用语言
            if (appLanguage == string.Empty)
                getAppLanguage();
            //创建语言文件夹
            if (!Directory.Exists(Path.Combine(path, appLanguage)))
                Directory.CreateDirectory(Path.Combine(path,appLanguage));
            foreach(string f in files)
            {
                await ResourcesHelper.writeResourcesFileToLocalMachine("Resources.Localization." + appLanguage + "." + f, Path.Combine(path, appLanguage, f));
            }
        }
        internal static List<string> getSupportLanguages(string type = "code")
        {
            Trace.WriteLine("mmmmm");
            List<string> list = new List<string>();
            foreach (LanguageInfo lang in AppConfig.Languages)
            {
                if (type == "code")
                    foreach(var tmp in lang.code)
                        list.Add(tmp);
                else
                    list.Add(lang.name);
            }
            return list;
        }
        internal static string getMainCodeFromOtherCode(string code)
        {
            foreach(LanguageInfo lang in AppConfig.Languages)
            {
                foreach(string  tmp in lang.code)
                {
                    if (tmp == code)
                    {
                        return lang.code[0];
                    }
                }
            }
            return null;
        }
        internal static string getLanguageProperty(string language)
        {
            foreach (LanguageInfo lang in AppConfig.Languages)
            {
                if(lang.name == language)
                {
                    return lang.code[0];
                }
                foreach (string tmp in lang.code)
                {
                    if (tmp == language) return lang.name;
                }
            }
            return null;
        }
        internal static void getAppLanguage()
        {
            //if (AppConfig.setting.language != string.Empty)
            //{
            //appLanguage = AppConfig.setting.language;
            //return;
            //}
            List<string> ls = getSupportLanguages(type:"code");
            foreach(var l in ls)
            {
                if (l == systemLanguage)
                {
                    appLanguage = getMainCodeFromOtherCode(systemLanguage);
                }
            }
            appLanguage = defaultLanguage;
        }
    }
}
