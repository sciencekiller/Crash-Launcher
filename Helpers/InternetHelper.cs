using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Crash_Launcher.DataStructure;
using static Crash_Launcher.DataStructure.Enums;

namespace Crash_Launcher.Helpers
{
    class InternetHelper
    {
        private static Stopwatch _stopWatch=new Stopwatch();
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(ref int Description, int ReservedValue);
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(ref Description, 0);
        }
        internal static string getProfileServerAddress(ProfileServers profileServers)
        {
            switch (profileServers)
            {
                case ProfileServers.Github:
                    return "https://raw.githubusercontent.com/sciencekiller/Crash-Launcher/main/";
                case ProfileServers.Gitlab:
                    return "https://gitlab.com/sciencekiller/Crash-Launcher/-/raw/main/";
                default:
                    return "https://gitlab.com/sciencekiller/Crash-Launcher/-/raw/main/";
            }
        }
        internal static async Task<bool> getFastestProfileServer()
        {
            long _fastestSpeed = long.MaxValue;
            ProfileServers _fastestServer=ProfileServers.None;
            foreach(ProfileServers server in Enum.GetValues(typeof(ProfileServers)))
            {
                if (server == ProfileServers.None)
                {
                    continue;
                }
                string serverAddress=getProfileServerAddress(server);
                HttpClient httpClient = new HttpClient();
                _stopWatch.Reset();
                _stopWatch.Start();
                await httpClient.GetByteArrayAsync(serverAddress+"ServerTest.txt");
                _stopWatch.Stop();
                if (_stopWatch.ElapsedMilliseconds < _fastestSpeed)
                {
                    _fastestServer = server;
                    _fastestSpeed = _stopWatch.ElapsedMilliseconds;
                }
            }
            if (_fastestServer == ProfileServers.None)
            {
                return false;
            }
            AppConfig.FastestProfileServer = _fastestServer;
            return true;
        }
    }
}
