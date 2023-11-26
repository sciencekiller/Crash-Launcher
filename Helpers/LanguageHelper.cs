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
        internal static string[] files = { "Initialize.resw" };
        private const string defaultLanguage = "en";
        internal static async Task getLocalizationInfo()
        {
            string inf = await ResourcesHelper.getStringFromResourcesFile("Resources.Localization.LocalizationInfo.json");
            AppConfig.Languages = JsonSerializer.Deserialize<List<LanguageInfo>>(inf);
        }
        internal static async Task checkLanguageFiles()
        {
            string path = Path.Combine(SystemEnvironmentHelper.SystemAppDataPath, "CrashLauncher", "Strings");
            //创建主文件夹
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //获取应用语言
            Trace.Write("m");
            if (AppConfig.Language == string.Empty)
            {
                getAppLanguage();
                Trace.WriteLine(AppConfig.Language);
            }
            //创建语言文件夹
            if (!Directory.Exists(Path.Combine(path, AppConfig.Language)))//stucked here
                Directory.CreateDirectory(Path.Combine(path, AppConfig.Language));
            string jsonContent = await ResourcesHelper.getStringFromResourcesFile("PrebuildData.LanguageFileHash.json");
            Trace.Write(jsonContent);
            List<LanguageChecksum> checksums = JsonSerializer.Deserialize<List<LanguageChecksum>>(jsonContent);
            foreach (string f in files)
            {
                string initcsm = CryptograghyHelper.getChecksumThroughLanguageCode(checksums, AppConfig.Language, "Initialize");//获取应该是的MD5值
                await ResourcesHelper.writeResourcesFileToLocalMachine("Resources.Localization." + AppConfig.Language + "." + f, Path.Combine(path, AppConfig.Language, f), initcsm);
            }
        }
        internal static List<string> getSupportLanguages(string type = "code")
        {
            List<string> list = new List<string>();
            foreach (LanguageInfo lang in AppConfig.Languages)
            {
                if (type == "code")
                    foreach (var tmp in lang.code)
                        list.Add(tmp);
                else
                    list.Add(lang.name);
            }
            return list;
        }
        internal static string getMainCodeFromOtherCode(string code)
        {
            foreach (LanguageInfo lang in AppConfig.Languages)
            {
                foreach (string tmp in lang.code)
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
                if (lang.name == language)
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
            List<string> ls = getSupportLanguages(type: "code");
            foreach (var l in ls)
            {
                if (l == systemLanguage)
                {
                    AppConfig.Language = getMainCodeFromOtherCode(systemLanguage);
                }
            }
            AppConfig.Language = defaultLanguage;
        }
    }
}
