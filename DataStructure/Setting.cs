using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash_Launcher.DataStructure
{
    internal class Setting
    {
        public string language { get; set; }
        public string dataPath { get; set; }
        public Enums.ProfileServers CDN {  get; set; }
        internal List<Enums.InitializeStep> checkSetting()
        {
            List<Enums.InitializeStep> steps=new List<Enums.InitializeStep>();
            if (language == string.Empty)
            {
                steps.Add(Enums.InitializeStep.Language);
            }
            if(dataPath == string.Empty)
            {
                steps.Add(Enums.InitializeStep.Path);
            }
            if (CDN == Enums.ProfileServers.None)
            {
                steps.Add (Enums.InitializeStep.CDN);
            }
            return steps;
        } 
    }
}
