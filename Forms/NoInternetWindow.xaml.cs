using WinUIEx;
using Crash_Launcher.Helpers;
using Windows.Graphics;
using System;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Crash_Launcher.Forms
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoInternetWindow : WindowEx
    {
        private const float Scale = 0.6f;
        public NoInternetWindow()
        {
            InitializeComponent();
            var appWindow = WindowHelper.GetAppWindowForCurrentWindow(this);
            appWindow.MoveAndResize(WindowHelper.getPosition(Scale));

        }
        private void changeTextOrder()
        {
            var currentLanguage = LanguageHelper.systemLanguage;
        }
    }
}
