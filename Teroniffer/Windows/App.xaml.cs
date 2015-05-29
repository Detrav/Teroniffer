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

namespace Detrav.Teroniffer.Windows
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mWindow;

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
            PacketCreator.setVersion(OpCodeVersion.P2805);
            mWindow = new MainWindow();
            MainWindow = mWindow;
            mWindow.Show();
            mWindow.close = true;

        }
    }
}
