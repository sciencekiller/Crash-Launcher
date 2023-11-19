using Crash_Launcher.DataStructure;
using Microsoft.UI.Xaml;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Crash_Launcher.Initialize
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitializeWindow : Window
    {
        public InitializeWindow()
        {
            InitializeComponent();
        }
        internal InitializeWindow(List<Enums.InitializeStep> needset)
        {
            InitializeComponent();

        }

    }
}
