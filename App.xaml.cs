// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.UI.Xaml;
using Crash_Launcher.Helpers;
using System.IO;
using Crash_Launcher.Forms;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Crash_Launcher.DataStructure;
using Windows.Storage;
using WinUI3Localizer;
using Windows.Security.Cryptography.Core;
using Crash_Launcher.Initialize;
using System.Collections.Generic;

namespace Crash_Launcher
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private bool isCollapsed = false;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new SplashWindow();
            (m_window.Content as FrameworkElement).Loaded += OnLoaded;
            m_window.Activate();
        }
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(SystemEnvironmentHelper.SystemAppDataPath);
            // 2. Check connection and show dialog
            (m_window.Content as FrameworkElement).Loaded -= OnLoaded;

            if (InternetHelper.IsConnectInternet() == false)
            {
                isCollapsed = true;
                var noWifiWindow = new NoInternetWindow();
                noWifiWindow.Activate();
                m_window.Close();
                m_window = noWifiWindow;
            }
            else if (await InternetHelper.getFastestProfileServer() == false)
            {
                isCollapsed = true;
                var noServerWindow = new ServerConnectionErrorWindow();
                noServerWindow.Activate();
                m_window.Close();
                m_window = noServerWindow;
            }
            SystemEnvironmentHelper.createAppDataDirectory();
            await LanguageHelper.getLocalizationInfo();
            await LanguageHelper.checkLanguageFiles();
            // 3. Show main window

            if (!isCollapsed)
            {
                await InitializeLocalizer();
                await InitForms();
            }
            //var mainWindow = new MainWindow();
            //mainWindow.Activate();

            // 4. Close "temp" window
            //m_window.Close();
            //m_window = mainWindow;
        }
        private async Task InitializeLocalizer()
        {
            // Initialize a "Strings" folder in the executables folder.
            string StringsFolderPath = Path.Combine(SystemEnvironmentHelper.SystemAppDataPath,"CrashLauncher", "Strings");
            StorageFolder stringsFolder = await StorageFolder.GetFolderFromPathAsync(StringsFolderPath);

            ILocalizer localizer = await new LocalizerBuilder()
                .AddStringResourcesFolderForLanguageDictionaries(StringsFolderPath)
                .SetOptions(options =>
                {
                    options.DefaultLanguage = LanguageHelper.appLanguage;
                })
                .Build();
        }
        private async Task InitForms()
        {
            Window tmpWindow=null;
            if (!File.Exists(Path.Combine(SystemEnvironmentHelper.SystemAppDataPath, "CrashLauncher", "config.json")))
            {
                tmpWindow = new InitializeWindow();
            }
            else
            {
                await AppConfigHelper.getAppSetting();
                List<Enums.InitializeStep> needSet = AppConfig.setting.checkSetting();
                if (needSet.Count > 0)
                {
                    tmpWindow = new InitializeWindow(needSet);
                }
            }
            m_window.Close();
            tmpWindow.Activate();
            m_window = tmpWindow;
        }

        private Window m_window;
    }
}
