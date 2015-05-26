using Detrav.TeraApi.OpCodes;
using Detrav.Teroniffer.Core;
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
            SortedList<string, object> sortedList = new SortedList<string, object>();
            foreach (var i in PacketCreator.getOpCodes())
                sortedList.Add(i.ToString(), i);
            comboBox.ItemsSource = null;
            comboBox.ItemsSource = sortedList.Values;
        }

        public StructureWindow(object select)
        {
            InitializeComponent();
            SortedList<string, object> sortedList = new SortedList<string, object>();
            foreach (var i in PacketCreator.getOpCodes())
                sortedList.Add(i.ToString(), i);
            comboBox.ItemsSource = null;
            comboBox.ItemsSource = sortedList.Values;
            comboBox.SelectedItem = select;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Insert(stackPanel.Children.Count - 1, new StructureElementControl(stackPanel.Children.Count - 1));
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            PacketStructure ps = new PacketStructure();
            for(int i =0; i<stackPanel.Children.Count-1;i++)
            {
                ps.elements.Add(((StructureElementControl)stackPanel.Children[i]).getPacketElement());
            }
            PacketStructureManager.setStructure(comboBox.SelectedItem, ps);
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            while (stackPanel.Children.Count > 1)
                stackPanel.Children.RemoveAt(0);
            PacketStructure ps = PacketStructureManager.getStructure(comboBox.SelectedItem);
            foreach(var el in ps.elements)
            {
                stackPanel.Children.Insert(stackPanel.Children.Count - 1, new StructureElementControl(stackPanel.Children.Count - 1,el));
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonLoad_Click(sender, new RoutedEventArgs());
        }
    }
}
