using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash_Launcher.DataStructure
{
    internal class Enums
    {
        public enum ProfileServers
        {
            None,
            Github,
            Gitlab,
            BitBucket
        };
        public enum InitializeStep
        {
            Language,
            Path,
            CDN
        }
    }
}
