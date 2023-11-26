using Crash_Launcher.DataStructure;
using Crash_Launcher.Helpers;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using WinUIEx;
using System.Diagnostics;
using WinRT.Interop;
using WinUI3Localizer;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Crash_Launcher.Initialize
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitializeWindow : WindowEx
    {
        private IntPtr hwnd;
        private AppWindow appWindow;
        private AppWindowTitleBar titleBar;
        private const double appScale = 0.8;
        private int totalSet;
        private int currentSet = 1;
        private List<Grid> grids = new List<Grid>();
        private ILocalizer localizer = Localizer.Get();
        private string dataPath = Path.Combine(AppContext.BaseDirectory, "Data");
        private List<Enums.InitializeStep> steps;
        public InitializeWindow(List<Enums.InitializeStep> needset)
        {
            steps = needset;
            InitializeComponent();
            //窗口设置
            //获取HWND、窗口和标题栏
            hwnd = WindowNative.GetWindowHandle(this);
            WindowId id = Win32Interop.GetWindowIdFromWindow(hwnd);
            appWindow = AppWindow.GetFromWindowId(id);
            titleBar = appWindow.TitleBar;
            //设置标题
            appWindow.Title = "Crash Launcher";
            //自定义标题栏
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                titleBar.ExtendsContentIntoTitleBar = true;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            //设置窗口显示大小
            var displayWidth = DisplayArea.Primary.WorkArea.Width;
            var displayHeight = DisplayArea.Primary.WorkArea.Height;
            var appWidth = displayWidth * appScale;
            var appHeight = displayHeight * appScale;
            appWindow.MoveAndResize(new Windows.Graphics.RectInt32((int)((displayWidth - appWidth) * 0.5), (int)((displayHeight - appHeight) * 0.5), (int)appWidth, (int)appHeight));
            //控件设置
            SelectedPath.Text = Path.Combine(AppContext.BaseDirectory, "Data");
            ListLanguages();
            AddGrids();
            grids[1].Visibility = Visibility.Visible;
        }

        private void ListLanguages()
        {
            List<string> languagelist = LanguageHelper.getSupportLanguages(type:"name");
            LanguageList.ItemsSource = languagelist;
            LanguageList.SelectedValue = LanguageHelper.getLanguageProperty(AppConfig.Language);
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSet == 1)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            if (currentSet == totalSet - 1)
            {
                NextStepButton.Visibility = Visibility.Collapsed;
                FinishButton.Visibility = Visibility.Visible;
            }
            NextSet();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSet == totalSet)
            {
                NextStepButton.Visibility = Visibility.Visible;
                FinishButton.Visibility = Visibility.Collapsed;
            }
            if (currentSet == 2)
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
            LastSet();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NextSet()
        {
            grids[currentSet].Visibility = Visibility.Collapsed;
            currentSet++;
            grids[currentSet].Visibility = Visibility.Visible;
        }
        private void LastSet()
        {
            grids[currentSet].Visibility = Visibility.Collapsed;
            currentSet--;
            grids[currentSet].Visibility = Visibility.Visible;
        }
        private void AddGrids()
        {
            grids.Add(new Grid());
            if (steps.Count == AppConfig.totalSet)
            {
                grids.Add(Welcome);
            }
            foreach(var st in steps)
            {
                grids.Add(getGridFromStepEnum(st));
            }
            totalSet = AppConfig.totalSet;
        }

        private async void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            InitializeWithWindow.Initialize(folderPicker, hwnd);
            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                dataPath = folder.Path;
                SelectedPath.Text = folder.Path;
            }
        }

        private async void LanguageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string changedlanguage = LanguageHelper.getLanguageProperty(LanguageList.SelectedValue.ToString());
            Trace.WriteLine(changedlanguage);
            AppConfig.Language = changedlanguage;
            await LanguageHelper.checkLanguageFiles();
            await localizer.SetLanguage(changedlanguage);
            
        }

        internal Grid getGridFromStepEnum(Enums.InitializeStep st)
        {
            return st switch
            {
                Enums.InitializeStep.Language => LanguageSet,
                Enums.InitializeStep.Path => DataPathSet,
                Enums.InitializeStep.CDN => CDNSet,
                _ => Welcome,
            };
        }
    }
}
