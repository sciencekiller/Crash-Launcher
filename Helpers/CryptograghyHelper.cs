using Crash_Launcher.DataStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crash_Launcher.Helpers
{
    internal class CryptograghyHelper
    {
        internal static string getSHA256FromFile(string path)
        {
            SHA256 sha256 = SHA256.Create();
            FileStream fileStream = File.Open(path, FileMode.Open);
            fileStream.Position = 0;
            byte[] cry = sha256.ComputeHash(fileStream);
            fileStream.Close();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < cry.Length; i++)
            {
                stringBuilder.Append(cry[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        internal static string getChecksumThroughLanguageCode(List<LanguageChecksum> ls,string code,string obj)
        {
            foreach(var tp in ls)
            {
                if (tp.code == code)
                {
                    switch (obj)
                    {
                        case "Initialize":
                            return tp.Initialize;
                        default:
                            return null;
                    }
                }
            }
            return null;
        }
    }
}
