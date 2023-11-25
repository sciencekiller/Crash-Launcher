using Crash_Launcher.DataStructure;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Crash_Launcher.Initialize
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitializeWindow : Window
    {
        private List<Enums.InitializeStep> steps;
        public InitializeWindow()
        {
            List<Enums.InitializeStep> needset = new List<Enums.InitializeStep>();
            foreach (Enums.InitializeStep st in Enum.GetValues(typeof(Enums.InitializeStep)))
            {
                needset.Add(st);
            }
            steps = needset;
            InitializeComponent();
        }
        internal InitializeWindow(List<Enums.InitializeStep> needset)
        {
            steps = needset;
            InitializeComponent();
        }

    }
}
