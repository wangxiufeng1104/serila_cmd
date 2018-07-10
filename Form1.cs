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
using System.Threading;
using YS.IO.Ports;

namespace serial_cmd
{

    public partial class Serial : Form
    {
        public static Serial SerialSingle = null;
        public static string softwareversion = " 0.1";
        private const byte CDMP_SOH = 0x7e;
        private const byte CDMP_VERSION = 0x10;
        SerialPortListener m_SerialPort = null;
        public Thread ThreadDataHandle;
        int pack = 0;
        bool IsInv = false;//是否处于盘点状态
       
        
        private struct INS_STRUCT
        {
            public byte[] ins_buf;
            public int ins_length;
            public int ins_end;
            public int ins_current;
        };
        enum CMD_TYPE
        {
            READ_ALL = 0,/*!< 读所有数据，要求设备上传所有数据*/
            READ_Date,    /*!< 读指定数据，要求设备上传指定数据内容*/
            WRITE_DATE,   /*!< 写指定数据，要求设备按照指定内容操作*/
            RESPOND_DATE, /*!< 响应数据，对于读报文，返回数据内容，对于写和升级报文，返回操作状态*/
            UPDATE_DATE,  /*!< 升级请求，对于同一次升级，Packet ID应相同*/
            UPDATE_RES,   /*!< 升级响应，响应Packet ID与请求的数值应相同*/
            INVENTORY     /*!< 盘点设备*/
        };
        enum DEVICE_TYPE
        {
            DEV_BOOT = 0,   /*!< 读所有数据，要求设备上传所有数据*/
            DEV_FAN_COIL,      /*!< 风机盘管*/
            DEV_FLOOR_HEAT,  /*!< 地暖*/
            DEV_FRESH,         /*!< 新风*/
            DEV_HUMID,       /*!< 加湿机*/
            DEV_DEHUMID      /*!< 除湿机*/
        };
        private INS_STRUCT ins_struct;
        private SerialPort m_s = null;
        private StopBits m_stopbits;
        private Parity m_parity;




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
            ins_init();

        }
        private void ins_init()
        {
            ins_struct.ins_buf = new byte[1024];
            ins_struct.ins_length = 0;
            ins_struct.ins_current = 0;
            ins_struct.ins_end = 0;
        }
        private void Serial_Load(object sender, EventArgs e)
        {

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
            TimeOut_Text.Text = "500";
        }
        
        
 
        private void PortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(m_SerialPort.IsOpen == false && PortName.Items.Count > 0)
            //{
            //    if (PortName.SelectedText != "")
            //        m_SerialPort.PortName = PortName.SelectedText;
            //}
            state_Lable.Text = "串口号：" + PortName.Text;
        }

        private void PortName_Click(object sender, EventArgs e)
        {
            
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
            state_Lable.Text = "波特率：" + BaudRate.Text;
        }

        private void But_OpenPort_Click(object sender, EventArgs e)
        {
            if (m_SerialPort == null)
            {
                m_SerialPort = new SerialPortListener(PortName.Text, Convert.ToInt32(BaudRate.Text));
                m_SerialPort.Parity = m_parity;
                m_SerialPort.StopBits = m_stopbits;
                m_SerialPort.DataBits = Convert.ToInt32(Com_DataBit.Text);
                m_SerialPort.BaudRate = Convert.ToInt32(BaudRate.Text);
                m_SerialPort.Handshake = Handshake.None;
                m_SerialPort.ReadBufferSize = 1024;
                m_SerialPort.ReceivedBytesThreshold = 1;
                m_SerialPort.BufferSize = 1024;
                m_SerialPort.ReceiveTimeout = Convert.ToInt32(TimeOut_Text.Text);
                m_SerialPort.WriteBufferSize = 100;
                m_SerialPort.SendInterval = 100;
                m_SerialPort.SerialPortResult += new HandResult(SerialPort_Result);
                m_SerialPort.OnSerialPortReceived += new OnReceivedData(SerialPort_Received);
                m_SerialPort.OnSeriaPortSend += new OnSendData(SerialPort_Send);
            }
            if (!m_SerialPort.IsOpen )
            {
                try
                {
                    m_SerialPort.Start();
                    But_OpenPort.Text = "关闭串口";
                    Console.WriteLine($"serialport1.PortName = {m_SerialPort.PortName}");
                    Console.WriteLine($"serialport1.BaudRate = {m_SerialPort.BaudRate}");
                    Console.WriteLine($"serialport1.DataBits = {m_SerialPort.DataBits}");
                    Console.WriteLine($"serialport1.StopBits = {m_SerialPort.StopBits}");
                    Console.WriteLine($"serialport1.Parity = {m_SerialPort.Parity}");
                    state_Lable.Text = "串口已经打开";


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ThreadDataHandle = new Thread(new ThreadStart(Protocol_Resolution));
                ThreadDataHandle.Priority = ThreadPriority.Lowest;
                ThreadDataHandle.Start();
            }
            else
            {
                m_SerialPort.Stop();
                if (m_SerialPort.IsOpen == false)
                    But_OpenPort.Text = "打开串口";
                state_Lable.Text = "串口已经打关闭";
            }
        }
        void SerialPort_Result(object sender, SerialPortEvents e)
        {
            this.Invoke(new MethodInvoker(() => {
                //处理结果
                //rtbResult.Text += Encoding.GetEncoding("GB2312").GetString(e.BufferData);
            }));
        }
        /// <summary>
        /// 串口接收数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialPort_Received(object sender, SerialPortEvents e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                long receivedCount = Convert.ToInt64(lbReceivedCount.Text);
                if (e.BufferData != null)
                {
                    receivedCount += e.BufferData.Length;
                }
                lbReceivedCount.Text = receivedCount.ToString();
                Console.Write("Data:");
                foreach(byte b in e.BufferData)
                {
                    Console.Write($"{b.ToString("x2")} ");
                }
                Console.WriteLine();

                InsCopy(e.BufferData, e.BufferData.Length);
                Console.WriteLine($"ins_struct.ins_length = {ins_struct.ins_length}");
            }));

        }
        void right_shift_current(int step)
        {
            int L_end;
            L_end = ins_struct.ins_buf.Length - ins_struct.ins_current;
            if (L_end >= step)
            {
                ins_struct.ins_current += step;
            }
            else
            {
                int L_start;
                L_start = step - L_end;
                ins_struct.ins_current = L_start;
            }
            if (ins_struct.ins_current >= 1024)
                ins_struct.ins_current = 0;
            ins_struct.ins_length -= step;
        }
        void Protocol_Resolution()
        {
            while(true)
            {
                if (ins_struct.ins_length >= 8)   //指令队列中存在未处理的指令
                {
                    //1、寻找SOH
                    if (CDMP_SOH == ins_struct.ins_buf[ins_struct.ins_current])//非SOH || 地址错误
                    {
                        int Frame_Length;
                        int L_end;
                        L_end = ins_struct.ins_buf.Length - ins_struct.ins_current;
                        if (L_end > 2)
                        {
                            Frame_Length = ins_struct.ins_buf[ins_struct.ins_current + 2];
                        }
                        else
                        {
                            Frame_Length = ins_struct.ins_buf[2 - L_end];
                        }
                        if (Frame_Length > ins_struct.ins_length)
                        {
                            right_shift_current(1);
                        }
                        else    //将数据移动出来做进一步的检查
                        {
                            UInt16 crc16;
                            byte[] P_frame = new byte[Frame_Length];
                            if (Frame_Length <= L_end)
                            {
                                Array.Copy(ins_struct.ins_buf, ins_struct.ins_current, P_frame, 0, Frame_Length);
                            }
                            else
                            {
                                Array.Copy(ins_struct.ins_buf, ins_struct.ins_current, P_frame, 0, L_end);
                                Array.Copy(ins_struct.ins_buf, 0, P_frame, L_end, Frame_Length - L_end);
                            }
                            crc16 = CRC16_MODBUS(P_frame, Frame_Length - 2);
                            if ((crc16 == ((P_frame[Frame_Length - 2] << 8) | (P_frame[Frame_Length - 1]))))
                            {
                                switch ((CMD_TYPE)P_frame[5])
                                {
                                    case CMD_TYPE.READ_ALL:
                                        Console.WriteLine("read all data\n");
                                        break;
                                    case CMD_TYPE.READ_Date:
                                        Console.WriteLine("read some data\n");
                                        break;
                                    case CMD_TYPE.WRITE_DATE:
                                        Console.WriteLine("write date\n");
                                        break;
                                    case CMD_TYPE.RESPOND_DATE:
                                        Console.WriteLine("respond data\n");
                                        break;
                                    case CMD_TYPE.UPDATE_DATE:
                                        Console.WriteLine("update date\n");
                                        break;
                                    case CMD_TYPE.UPDATE_RES:
                                        Console.WriteLine("update respoond\n");
                                        break;
                                    case CMD_TYPE.INVENTORY://盘点设备
                                                            //Device_List.Text += "盘点设备：\r\n";
                                        string Device_inf = "设备地址：";
                                        Device_inf += ("0x" + P_frame[3].ToString("x2")) + " ";
                                        Device_inf += "设备类型：";
                                        //Device_inf += pack.ToString();
                                        switch ((DEVICE_TYPE)P_frame[6])
                                        {
                                            case DEVICE_TYPE.DEV_BOOT:
                                                Device_inf += "Boot Loader\r\n";
                                                break;
                                            case DEVICE_TYPE.DEV_FAN_COIL:
                                                Device_inf += "风机盘管\r\n";
                                                break;
                                            case DEVICE_TYPE.DEV_FLOOR_HEAT:
                                                Device_inf += "地暖\r\n";
                                                break;
                                            case DEVICE_TYPE.DEV_FRESH:
                                                Device_inf += "新风\r\n";
                                                break;
                                            case DEVICE_TYPE.DEV_HUMID:
                                                Device_inf += "加湿机\r\n";
                                                break;
                                            case DEVICE_TYPE.DEV_DEHUMID:
                                                Device_inf += "除湿机\r\n";
                                                break;
                                            default:
                                                Device_inf += "未定义设备\r\n";
                                                break;

                                        }
                                        this.Invoke(new MethodInvoker(() =>
                                        {
                                            Device_List.AppendText(Device_inf); ;
                                            Device_List.ScrollToCaret();
                                        }));
                                        
                                        Console.WriteLine($"pack = {pack}");
                                        pack++;
                                        break;
                                    default:
                                        Console.WriteLine("invalid cmd\n");
                                        break;
                                }
                                right_shift_current(Frame_Length);
                            }
                            else
                            {
                                right_shift_current(1);
                            }
                        }
                    }
                    else //向后移动
                    {
                        right_shift_current(1);
                    }
                }
            }
        }
        void InsCopy(byte[] pSrc, int Len)
        {
            //1、判断复制的方式   a 正常的复制   b 到指令缓存的尾部环形复制   c 溢出，舍弃指令
            int Last_Size;//指令缓存中剩余的空间
            int L_end, L_start;
            Last_Size = ins_struct.ins_buf.Length - ins_struct.ins_end;
            if (Last_Size >= Len)  //a
            {
                Array.Copy(pSrc, 0, ins_struct.ins_buf, ins_struct.ins_end, Len);
                ins_struct.ins_end = ins_struct.ins_end + Len;//更新地址
                ins_struct.ins_length += Len;
            }
            else
            {
                L_end = Last_Size;         //尾部剩余空间
                L_start = Len - Last_Size; //头部剩余空间
                                           //判断是否溢出
                if (L_start < ins_struct.ins_current)  //b
                {
                    Array.Copy(pSrc, 0, ins_struct.ins_buf, ins_struct.ins_end, L_end);
                    Array.Copy(pSrc, L_end, ins_struct.ins_buf, 0, L_start);
                    
                    ins_struct.ins_end = L_start;//更新地址
                    ins_struct.ins_length += Len;
                }
                //溢出不做任何事情，数据自动被覆盖      c
            }
            if (ins_struct.ins_end >= 1024)
                ins_struct.ins_end = 0;
            
            //Console.WriteLine($"ins_struct.ins_length = {ins_struct.ins_length}");
        }
        
        void SerialPort_Send(object sender, SerialPortEvents e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                long sendCount = Convert.ToInt64(lbSendCount.Text);
                if (e.BufferData != null)
                {
                    sendCount += e.BufferData.Length;
                }
                lbSendCount.Text = sendCount.ToString();
            }));
        }
        //注释：    先CTRL+K，然后CTRL+C
        //取消注释： 先CTRL+K，然后CTRL+U


        private void Com_DataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            state_Lable.Text = "数据位：" + Com_DataBit.Text;
        }

        private void Com_StopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = Com_StopBit.SelectedIndex;
            
            switch (index)
            {
                case 0:
                    m_stopbits = StopBits.One;
                    break;
                case 1:
                    m_stopbits = StopBits.Two;
                    break;
                default:
                    m_stopbits = StopBits.One;
                    break;
            }
            Console.WriteLine($"serialPort1.StopBits = {m_stopbits}");
            state_Lable.Text = "停止位：" + m_stopbits.ToString();
        }

        private void Com_CheckBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 index = Com_CheckBit.SelectedIndex;
            switch (index)
            {
                case 0:
                    m_parity = Parity.None;
                    break;
                case 1:
                    m_parity = Parity.Odd;
                    break;
                case 2:
                    m_parity = Parity.Even;
                    break;
                default:
                    m_parity = Parity.None;
                    break;
            }
            Console.WriteLine($"serialPort1.Parity = {m_parity}");
            state_Lable.Text = "校验位：" + m_parity.ToString();
        }

        private void But_Inventory_Device_Click(object sender, EventArgs e)
        {
            
            if (!m_SerialPort.IsOpen)
            {
                return;
            }
            IsInv = true;
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
                wChar = InvertUint8(wChar);
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
                IsInv = false;
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
            add_max++;
            m_SerialPort.UseEndChar = false;
            m_SerialPort.Send(Inventory.ToArray());
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

        private void Send_Data_Click(object sender, EventArgs e)
        {
            //byte[] data = Encoding.GetEncoding("GB2312").GetBytes(rtbResult.Text);
            m_SerialPort.UseEndChar = false;
            //m_SerialPort.Send(data);
        }

        private void But_Clear_Click(object sender, EventArgs e)
        {
            Device_List.Clear();
            lbReceivedCount.Text = "0";
            lbSendCount.Text = "0";
        }
    }
}
