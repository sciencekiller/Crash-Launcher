using WinUIEx;
using Crash_Launcher.Helpers;
using Windows.Graphics;
using System;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.ApplicationModel.Core;
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
            SystemBackdrop = WindowHelper.getBackDrop();
            var titleBar = appWindow.TitleBar;
            titleBar.ExtendsContentIntoTitleBar = true;
            titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        }

        private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            Close();
        }
    }
}
