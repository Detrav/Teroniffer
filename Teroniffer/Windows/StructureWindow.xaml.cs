using Detrav.Teroniffer.UserElements;
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
    /// Логика взаимодействия для StructureWindow.xaml
    /// </summary>
    public partial class StructureWindow : Window
    {
        public StructureWindow()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Insert(stackPanel.Children.Count - 1, new StructureElementControl(stackPanel.Children.Count - 1));
        }
    }
}
