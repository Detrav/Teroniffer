﻿using System;
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
        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            mWindow = (Application.Current.MainWindow as MainWindow);
        }
    }
}
