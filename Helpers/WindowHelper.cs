using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics;
using WinRT.Interop;

namespace Crash_Launcher.Helpers
{
    internal class WindowHelper
    {
        private static int displayWidth = DisplayArea.Primary.WorkArea.Width;
        private static int displayHeight = DisplayArea.Primary.WorkArea.Height;
        internal static RectInt32 getPosition(double scale)
        {
            var appHeight = displayHeight * scale;
            var appWidth= displayWidth * scale;
            return new RectInt32((int)((displayWidth - appWidth) * 0.5), (int)((displayHeight - appHeight) * 0.5), (int)appWidth, (int)appHeight);  
        }
        internal static SystemBackdrop getBackDrop()
        {
            if (MicaController.IsSupported())
            {
                return new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            }
            else
            {
                return new DesktopAcrylicBackdrop();
            }
        }
        internal static AppWindow GetAppWindowForCurrentWindow(Window w)
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(w);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }
    }
}
