using Detrav.TeraApi;
using Detrav.Teroniffer.Core;
using System;
using System.Collections;
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
    /// Логика взаимодействия для ViewPacketWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
            foreach (var el in PacketElement.types)
                comboBoxType.Items.Add(el);
            comboBoxType.SelectedIndex = 0;
        }

        private void textBoxHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_preventTextBoxEvents)
                return;
            _preventTextBoxEvents = true;
            try
            {
                try
                {
                    switch (comboBoxType.SelectedItem.ToString())
                    {
                        case "byte": textBoxValue.Text = TeraPacketWithData.toByte(TeraPacketWithData.fromHex(textBoxHex.Text),0).ToString(); break;
                        case "sbyte": textBoxValue.Text = TeraPacketWithData.toSByte(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "ushort": textBoxValue.Text = TeraPacketWithData.toUInt16(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "short": textBoxValue.Text = TeraPacketWithData.toInt16(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "uint": textBoxValue.Text = TeraPacketWithData.toUInt32(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "int": textBoxValue.Text = TeraPacketWithData.toInt32(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "ulong": textBoxValue.Text = TeraPacketWithData.toUInt64(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "long": textBoxValue.Text = TeraPacketWithData.toInt64(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "float": textBoxValue.Text = TeraPacketWithData.toSingle(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "double": textBoxValue.Text = TeraPacketWithData.toDouble(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "singleChar": textBoxValue.Text = TeraPacketWithData.toSingleChar(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "doubleChar": textBoxValue.Text = TeraPacketWithData.toDoubleChar(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "singleString": textBoxValue.Text = TeraPacketWithData.toSingleString(TeraPacketWithData.fromHex(textBoxHex.Text), 0,10000); break;
                        case "doubleString": textBoxValue.Text = TeraPacketWithData.toDoubleString(TeraPacketWithData.fromHex(textBoxHex.Text), 0,10000); break;
                        case "bool": textBoxValue.Text = TeraPacketWithData.toBoolean(TeraPacketWithData.fromHex(textBoxHex.Text), 0).ToString(); break;
                        case "hex": textBoxValue.Text = TeraPacketWithData.toHex(TeraPacketWithData.fromHex(textBoxHex.Text)); break;
                    }
                }
                catch { textBoxValue.Text = "ERROR"; }
            }
            catch { }
            _preventTextBoxEvents = false;
        }
        bool _preventTextBoxEvents = false;
        private void textBoxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_preventTextBoxEvents)
                return;
            _preventTextBoxEvents = true;
            try
            {
                try
                {
                    switch (comboBoxType.SelectedItem.ToString())
                    {
                        case "byte": textBoxHex.Text = byte.Parse(textBoxValue.Text).ToString("X2"); break;
                        case "sbyte": textBoxHex.Text = sbyte.Parse(textBoxValue.Text).ToString("X2"); break;
                        case "ushort": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(ushort.Parse(textBoxValue.Text))); break;
                        case "short": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(short.Parse(textBoxValue.Text))); break;
                        case "uint": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(uint.Parse(textBoxValue.Text))); break;
                        case "int": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(int.Parse(textBoxValue.Text))); break;
                        case "ulong": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(ulong.Parse(textBoxValue.Text))); break;
                        case "long": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(long.Parse(textBoxValue.Text))); break;
                        case "float": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(float.Parse(textBoxValue.Text))); break;
                        case "double": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(double.Parse(textBoxValue.Text))); break;
                        case "singleChar": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(char.Parse(textBoxValue.Text))); break;
                        case "doubleChar": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(char.Parse(textBoxValue.Text))); break;
                        case "singleString": textBoxHex.Text = TeraPacketWithData.toHex(TeraPacketWithData.fromSingleString(textBoxValue.Text)); break;
                        case "doubleString": textBoxHex.Text = TeraPacketWithData.toHex(TeraPacketWithData.fromDoubleString(textBoxValue.Text)); break;
                        case "bool": textBoxHex.Text = TeraPacketWithData.toHex(BitConverter.GetBytes(bool.Parse(textBoxValue.Text))); break;
                        case "hex": textBoxHex.Text = TeraPacketWithData.toHex(TeraPacketWithData.fromHex(textBoxValue.Text)); break;
                    }
                }
                catch { textBoxHex.Text = "ERROR"; }
            }
            catch { }
            _preventTextBoxEvents = false;
        }


        /*
        public void setData(byte[] data)
        {
            this.data = data;
            reMake();
        }
        /*public void reMake()
        {
            try
            {
                string str = "{0:X4} - {1} : {2}\n\n{3}";
                string str_with_shift = "{0:X4}+{1} - {2} : {3}\n\n{4}";
                try
                {
                    ushort start = UInt16.Parse(numericBoxStart.Text);
                    ushort size = UInt16.Parse(numericBoxSize.Text);
                    string type = comboBoxType.SelectedItem.ToString();
                    switch (type)
                    {
                        case "bitarray":
                            BitArray ba = new BitArray(new byte[1] { data[start] });
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < ba.Length; i++)
                                if (ba[i]) sb.Append("1");
                                else
                                    sb.Append("0");
                            str = String.Format(str, start, sb.ToString(), type, TeraPacketWithData.toHex(data, start, start + 1));
                            break;
                        case "byte": str = String.Format(str, start, data[start], type, TeraPacketWithData.toHex(data, start, start+1)); break;
                        case "sbyte": str = String.Format(str, start, (sbyte)data[start], type, TeraPacketWithData.toHex(data, start, start + 1)); break;
                        case "ushort": str = String.Format(str, start, BitConverter.ToUInt16(data, start), type, TeraPacketWithData.toHex(data, start, start + 2)); break;
                        case "short": str = String.Format(str, start, BitConverter.ToInt16(data, start), type, TeraPacketWithData.toHex(data, start, start + 2)); break;
                        case "uint": str = String.Format(str, start, BitConverter.ToUInt32(data, start), type, TeraPacketWithData.toHex(data, start, start + 4)); break;
                        case "int": str = String.Format(str, start, BitConverter.ToInt32(data, start), type, TeraPacketWithData.toHex(data, start, start + 4)); break;
                        case "ulong": str = String.Format(str, start, BitConverter.ToUInt64(data, start), type, TeraPacketWithData.toHex(data, start, start + 8)); break;
                        case "long": str = String.Format(str, start, BitConverter.ToInt64(data, start), type, TeraPacketWithData.toHex(data, start, start + 8)); break;
                        case "float": str = String.Format(str, start, BitConverter.ToSingle(data, start), type, TeraPacketWithData.toHex(data, start, start + 4)); break;
                        case "double": str = String.Format(str, start, BitConverter.ToDouble(data, start), type, TeraPacketWithData.toHex(data, start, start + 8)); break;
                        case "char": str = String.Format(str, start, BitConverter.ToChar(data, start), type, TeraPacketWithData.toHex(data, start, start + 1)); break;
                        case "string": str = String.Format(str, start, TeraPacketWithData.toDoubleString(data, start, size+start), type, TeraPacketWithData.toHex(data, start, TeraPacketWithData.toDoubleString(data, start,size+start).Length)); break;
                        case "boolean": str = String.Format(str, start, BitConverter.ToBoolean(data, start), type, TeraPacketWithData.toHex(data, start, start + 1)); break;
                        case "hex": str = String.Format(str, start, TeraPacketWithData.toDoubleString(data, start, size + start), type, TeraPacketWithData.toHex(data, start, start + size)); break;
                        default: str = String.Format(str_with_shift, start, size, "unknown", type); break;
                    }
                }
                catch (Exception e)
                {
                    str = e.Message;
                }
                textBox.Text = str;
            }
            catch { }
        }

        private void Button_Click_plus1(object sender, RoutedEventArgs e)
        {
            int v = 0;
            Int32.TryParse(numericBoxStart.Text, out v);
            numericBoxStart.Text = (v + 1).ToString();
        }
        private void Button_Click_minus1(object sender, RoutedEventArgs e)
        {
            int v = 0;
            Int32.TryParse(numericBoxStart.Text, out v);
            numericBoxStart.Text = (v - 1).ToString();
        }
        private void Button_Click_plus2(object sender, RoutedEventArgs e)
        {
            int v = 0;
            Int32.TryParse(numericBoxSize.Text, out v);
            numericBoxSize.Text = (v + 1).ToString();
        }
        private void Button_Click_minus2(object sender, RoutedEventArgs e)
        {
            int v = 0;
            Int32.TryParse(numericBoxSize.Text, out v);
            numericBoxSize.Text = (v - 1).ToString();
        }

        private void numericBoxStart_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                int v = 0;
                Int32.TryParse(numericBoxStart.Text, out v);
                numericBoxStart.Text = (v + 1).ToString();
            }
            else if (e.Key == Key.Down)
            {
                int v = 0;
                Int32.TryParse(numericBoxStart.Text, out v);
                numericBoxStart.Text = (v - 1).ToString();
            }
        }

        private void numericBoxSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                int v = 0;
                Int32.TryParse(numericBoxSize.Text, out v);
                numericBoxSize.Text = (v + 1).ToString();
            }
            else if (e.Key == Key.Down)
            {
                int v = 0;
                Int32.TryParse(numericBoxSize.Text, out v);
                numericBoxSize.Text = (v - 1).ToString();
            }
        }

        private void numericBoxSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            reMake();
        }

        private void numericBoxStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            reMake();
        }

        private void comboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reMake();
    }
}
        }
        */
    }
}