using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Crash_Launcher.Helpers
{
    internal class ResourcesHelper
    {
        internal static async Task<string> getStringFromResourcesFile(string file)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            string resourcesPath = "Crash_Launcher." + file;
            Stream stream=_assembly.GetManifestResourceStream(resourcesPath);
            StreamReader sr = new StreamReader(stream);
            string opt=await sr.ReadToEndAsync();
            stream.Close();
            sr.Close();
            return opt;
        }
        internal static async Task writeResourcesFileToLocalMachine(string rspath,string optpath)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            string resourcesPath = "Crash_Launcher." + rspath;
            Trace.WriteLine(resourcesPath);
            Stream stream = _assembly.GetManifestResourceStream(resourcesPath);
            using(var fs=File.Create(optpath))
            {
                await stream.CopyToAsync(fs);
            }
            stream.Close();
        }
    }
}
