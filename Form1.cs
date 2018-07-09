using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace serial_cmd
{
    public partial class Serial : Form
    {
        public static Serial SerialSingle = null;
        public static string softwareversion = " 0.1";
        private const byte CDMP_SOH = 0x7e;
        private const byte CDMP_VERSION = 0x10;
        
        public Serial()
        {
            SerialSingle = this;
            InitializeComponent();
            SOH_Text.MaxLength = 2;
            SOH_Text.Text = "7e";
            VER_Text.MaxLength = 2;
            VER_Text.Text = "10";




            PUD_Text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            PUD_Text.KeyPress += numberTextBox_KeyPress;
            this.Text = this.Text + softwareversion;
            serilal_defaultSetting();

        }
        private void numberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F') || (e.KeyChar == (char)8) || (e.KeyChar == 0x20))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public static Serial GetSingle()
        {
            if (SerialSingle == null)
            {
                SerialSingle = new Serial();

            }
            return SerialSingle;
        }
        private void serilal_defaultSetting()
        {
            BaudRate.SelectedIndex = 7;
            Com_DataBit.SelectedIndex = 3;
            Com_StopBit.SelectedIndex = 0;
            Com_CheckBit.SelectedIndex = 0;
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;
            serialPort1.Handshake = Handshake.None;
            serialPort1.RtsEnable = true;

        }
        private void PortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen == false && PortName.Items.Count > 0)
            {
                if (PortName.SelectedText != "")
                    serialPort1.PortName = PortName.SelectedText;
            }
            state_Lable.Text = "串口号：" + serialPort1.PortName.ToString();
        }

        private void PortName_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                But_OpenPort.Text = "打开串口";
            }
            string[] PortStr = SerialPort.GetPortNames();
            if (PortStr == null)
            {
                state_Lable.Text = "Error: No Serial";
                return;
            }
            PortName.Items.Clear();
            foreach (string s in PortStr)
            {
                PortName.Items.Add(s);
            }
            if (PortName.Items.Count > 0)
            {
                PortName.SelectedIndex = 0;
            }
        }

        private void BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            String Sbaud = BaudRate.Text;
            Console.WriteLine($"Sbaud = {Sbaud}");
            if (Sbaud != "")
            {
                serialPort1.BaudRate = Convert.ToInt32(Sbaud);
            }
            state_Lable.Text = "波特率：" + serialPort1.BaudRate.ToString();
        }

        private void But_OpenPort_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen && PortName.SelectedText != null)
            {
                try
                {
                    serialPort1.PortName = PortName.Text;
                    serialPort1.Open();
                    But_OpenPort.Text = "关闭串口";
                    Console.WriteLine($"serialport1.PortName = {serialPort1.PortName}");
                    Console.WriteLine($"serialport1.BaudRate = {serialPort1.BaudRate}");
                    Console.WriteLine($"serialport1.DataBits = {serialPort1.DataBits}");
                    Console.WriteLine($"serialport1.StopBits = {serialPort1.StopBits}");
                    Console.WriteLine($"serialport1.Parity = {serialPort1.Parity}");
                    state_Lable.Text = "串口已经打开";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                serialPort1.Close();
                if (serialPort1.IsOpen == false)
                    But_OpenPort.Text = "打开串口";
                state_Lable.Text = "串口已经打关闭";
            }
        }

        private void Com_DataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            String DataBit = Com_DataBit.Text;
            Console.WriteLine($"DataBit = {DataBit}");
            if (DataBit != "")
            {
                serialPort1.DataBits = Convert.ToInt32(DataBit);
            }
            state_Lable.Text = "数据位：" + serialPort1.DataBits.ToString();
        }

        private void Com_StopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = Com_StopBit.SelectedIndex;
            
            switch (index)
            {
                case 0:
                    serialPort1.StopBits = StopBits.One;
                    break;
                case 1:
                    serialPort1.StopBits = StopBits.Two;
                    break;
                default:
                    serialPort1.StopBits = StopBits.One;
                    break;
            }
            Console.WriteLine($"serialPort1.StopBits = {serialPort1.StopBits}");
            state_Lable.Text = "停止位：" + serialPort1.StopBits.ToString();
        }

        private void Com_CheckBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = Com_CheckBit.SelectedIndex;
            switch (index)
            {
                case 0:
                    serialPort1.Parity = Parity.None;
                    break;
                case 1:
                    serialPort1.Parity = Parity.Odd;
                    break;
                case 2:
                    serialPort1.Parity = Parity.Even;
                    break;
                default:
                    serialPort1.Parity = Parity.None;
                    break;
            }
            Console.WriteLine($"serialPort1.Parity = {serialPort1.Parity}");
            state_Lable.Text = "校验位：" + serialPort1.Parity.ToString();
        }

        private void But_Inventory_Device_Click(object sender, EventArgs e)
        {
            
            if (!serialPort1.IsOpen)
            {
                return;
            }
            timer1.Enabled = true;
            timer1.Start(); //

            //serialPort1.Write(ByteArrary,0,ByteArrary.Length);
        }
        private UInt16 crc16_modbus(byte[] modbusdata, int length)
        {
            int i, j;

            UInt16 crc = 0xffff;
            try
            {
                for (i = 0; i < length; i++)
                {
                    crc ^= modbusdata[i];
                    for (j = 0; j < 8; j++)
                    {
                        if ((crc & 0x01) == 1)
                        {
                            crc = (UInt16)((crc >> 1) ^ 0xa001);
                        }
                        else
                        {
                            crc >>= 1;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return crc;
        }
        private byte InvertUint8(byte srcBuf)
        {
            int i;
            byte tmp = 0;
            for (i = 0; i < 8; i++)
            {
                if ((srcBuf & (1 << i)) > 0)
                    tmp |= (byte)(1 << (7 - i));
            }
            return tmp;
        }
        private UInt16 InvertUint16(UInt16 srcBuf)
        {
            int i;
            UInt16 tmp = 0;
            for (i = 0; i < 16; i++)
            {
                if ((srcBuf & (1 << i)) > 0)
                    tmp |= (UInt16)(1 << (15 - i));
            }
            return tmp;
        }
        UInt16 CRC16_MODBUS(byte[] puchMsg, int usDataLen)
        {
            UInt16 wCRCin = 0xFFFF;
            UInt16 wCPoly = 0x8005;
            byte wChar = 0;
            for (int i = 0; i < usDataLen; i++)
            {
                wChar = puchMsg[i];
                Console.WriteLine($"before wChar = {wChar.ToString("x")}");
                wChar = InvertUint8(wChar);
                Console.WriteLine($"after wChar = {wChar.ToString("x")}");
                wCRCin ^= (UInt16)(wChar << 8);
                for(int j = 0; j < 8; j++)
                {
                    if ((wCRCin & 0x8000) > 0)
                        wCRCin = (UInt16)((wCRCin << 1) ^ wCPoly);
                    else
                        wCRCin = (UInt16)(wCRCin << 1);
                }
            }
            wCRCin = InvertUint16(wCRCin);
            return (wCRCin);
        }
        public byte add_max = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            UInt16 crc16;
            if (add_max >= 16)
            {
                add_max = 0;
                timer1.Stop();
                timer1.Enabled = false;
            }
            List<byte> Inventory = new List<byte> { };
            Inventory.Add(CDMP_SOH); //
            Inventory.Add(CDMP_VERSION);
            Inventory.Add(9); //length
            Inventory.Add(add_max);
            Inventory.Add(0);
            Inventory.Add(0x06);
            Inventory.Add(0x00);
            crc16 = crc16_modbus(Inventory.ToArray(), Inventory.ToArray().Length);
            Inventory.Add((byte)(crc16 >> 8));
            Inventory.Add((byte)crc16);
            serialPort1.Write(Inventory.ToArray(), 0, Inventory.ToArray().Length);
            add_max++;
            //System.Threading.Thread.Sleep(1000);
        }

        private void Serial_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PUD_Text_TextChanged(object sender, EventArgs e)
        {
            if(PUD_Text.TextLength > 0)
            {
                char[] chars;
                string str1;
                int i = 0;
                Console.Write($"Length = {PUD_Text.TextLength}  ");
                string trim = Regex.Replace(PUD_Text.Text, @"\s", "");
                if((trim.Length % 2) == 1)
                {
                    str1 = trim.Substring(0,trim.Length - 1);
                }
                else
                {
                    str1 = trim;
                }
                chars = str1.ToCharArray();
                Console.WriteLine(chars);
                foreach(char c in chars)
                {
                    Console.Write($"{c} ");
                }
                Console.WriteLine();
                string[] chartostring = new string[chars.Length/2];
                byte[] num = new byte[chars.Length/2];
                for ( i = 0;i < chars.Length/2;i ++)
                {
                    chartostring[i] = chars[i * 2].ToString() + chars[i * 2 + 1].ToString();
                }
                i = 0;
                foreach(string s in chartostring)
                {
                    Console.Write($"{s} ");
                    num[i] = Byte.Parse(Byte.Parse(s,System.Globalization.NumberStyles.HexNumber).ToString("X2"), 
                                            System.Globalization.NumberStyles.HexNumber);
                    i++;
                }
                Console.WriteLine();
                foreach (byte b in num)
                {
                    Console.Write($"{b} ");
                    
                }
                Console.WriteLine();
            }
        }

    }
}
