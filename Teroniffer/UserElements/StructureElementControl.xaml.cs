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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Detrav.Teroniffer.UserElements
{
    /// <summary>
    /// Логика взаимодействия для StructureElementControl.xaml
    /// </summary>
    public partial class StructureElementControl : UserControl
    {
        public int num
        {
            get
            {
                return (int)labelNum.Content;
            }
            set
            {
                labelNum.Content = value;
            }
        }
        public StructureElementControl(int i = -1)
        {
            InitializeComponent();
            num = i;
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = Parent as StackPanel;
            sp.Children.RemoveAt(num);
            for (int i = num; i < sp.Children.Count-1;i++ )
            {
                (sp.Children[i] as StructureElementControl).num = i;
            }
        }
    }
}
