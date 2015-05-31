using Detrav.TeraApi.OpCodes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Detrav.Teroniffer.Windows
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mWindow;
        DispatcherTimer timer;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var cofd = new CommonOpenFileDialog();
            cofd.IsFolderPicker = true;
            cofd.InitialDirectory = System.IO.Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory);
            if (cofd.ShowDialog() != CommonFileDialogResult.Ok)
            {
                Shutdown();
                return;
            }
            Core.PacketStructureManager.assets = new Core.AssetManager(cofd.FileName);
            PacketCreator.setVersion(OpCodeVersion.P2904);
            mWindow = new MainWindow();
            MainWindow = mWindow;
            mWindow.Show();
            mWindow.close = true;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            mWindow.doEvents();
            timer.Start();
        }
    }
}
