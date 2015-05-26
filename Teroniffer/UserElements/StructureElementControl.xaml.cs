using Detrav.Teroniffer.Core;
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
        public StructureElementControl(int i)
        {
            InitializeComponent();
            num = i;
            foreach (var el in PacketElement.types)
                comboBoxType.Items.Add(el);
            comboBoxType.SelectedIndex = 0;
        }

        internal StructureElementControl(int i, PacketElement pe) :this(i)
        {
            textBoxName.Text = pe.name;
            textBoxStart.Text = pe.start;
            textBoxEnd.Text = pe.end;
            comboBoxType.SelectedItem = pe.type;
        }

        internal PacketElement getPacketElement()
        {
            return new PacketElement()
            {
                name = textBoxName.Text,
                start = textBoxStart.Text,
                end = textBoxEnd.Text,
                type = comboBoxType.SelectedItem.ToString()
            };
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

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if (num <= 0) return;
            StackPanel sp = Parent as StackPanel;
            sp.Children.RemoveAt(num);
            sp.Children.Insert(num - 1, this);
            num--;
            (sp.Children[num + 1] as StructureElementControl).num = num + 1;
        }

        private void ButtonDwn_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = Parent as StackPanel;
            if (num >= sp.Children.Count - 1) return;
            sp.Children.RemoveAt(num);
            sp.Children.Insert(num + 1, this);
            num++;
            (sp.Children[num - 1] as StructureElementControl).num = num - 1;
        }
    }
}
