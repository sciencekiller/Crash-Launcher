using System.Runtime.InteropServices;

namespace Crash_Launcher.Helpers
{
    class InternetHelper
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(ref int Description, int ReservedValue);
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(ref Description, 0);
        }
        enum ProfileServers
        {
            Github,
            Gitee,
            Gitlab
        };
        private string getProfileServerAddress(ProfileServers profileServers)
        {
            switch (profileServers)
            {
                case ProfileServers.Github:
                    return "https://raw.githubusercontent.com/sciencekiller/Crash-Launcher/main/";
                case ProfileServers.Gitee:
                    return "https://gitee.com/sciencekiller/Crash-Launcher/raw/main/";
            }
        }
    }
}
