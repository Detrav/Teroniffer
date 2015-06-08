using Detrav.TeraApi.OpCodes;
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
            comboBox.SelectedIndex = 0;
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
            
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            PacketStructure ps = new PacketStructure(false);
            ps.script = textBox.Text;
            PacketStructureManager.setStructure(comboBox.SelectedItem, ps);
            MessageBox.Show("Сохранено");
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            PacketStructureManager.loadStructure(comboBox.SelectedItem);
            PacketStructure ps = PacketStructureManager.getStructure(comboBox.SelectedItem);
            textBox.Text = ps.script;
            MessageBox.Show("Загруженно");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PacketStructureManager.loadStructure(comboBox.SelectedItem);
            PacketStructure ps = PacketStructureManager.getStructure(comboBox.SelectedItem);
            textBox.Text = ps.script;
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PacketStructure ps = new PacketStructure(false);
                ps.script = textBox.Text;
                byte[] data = new byte[1000];
                for(int i = 0; i<data.Length;i+=2)
                {
                    byte[] bb = BitConverter.GetBytes((ushort)2);
                    data[i] = bb[0];
                    data[i + 1] = bb[1];
                }
                ps.parse(new TeraApi.TeraPacketWithData(data, TeraApi.PacketType.Any));
                MessageBox.Show("OK");
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Хотите сохранить изменения в текущем пакете?", "Закрытие", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    PacketStructure ps = new PacketStructure(false);
                    ps.script = textBox.Text;
                    PacketStructureManager.setStructure(comboBox.SelectedItem, ps);
                    break;
                case MessageBoxResult.No: break;
                default: e.Cancel = true; break;
            }
        }
    }
}
