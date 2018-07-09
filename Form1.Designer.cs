namespace serial_cmd
{
    partial class Serial
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Serial));
            this.label1 = new System.Windows.Forms.Label();
            this.PortName = new System.Windows.Forms.ComboBox();
            this.BaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.But_OpenPort = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.state_Lable = new System.Windows.Forms.ToolStripStatusLabel();
            this.Com_DataBit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Com_CheckBit = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Com_StopBit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.But_Inventory_Device = new System.Windows.Forms.Button();
            this.Device_List = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.PUD_Text = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Make_Data = new System.Windows.Forms.Button();
            this.Send_Data = new System.Windows.Forms.Button();
            this.CRC_Text = new serial_cmd.HexNumberTextBox(this.components);
            this.CMD_Text = new serial_cmd.HexNumberTextBox(this.components);
            this.Len_Text = new serial_cmd.HexNumberTextBox(this.components);
            this.VER_Text = new serial_cmd.HexNumberTextBox(this.components);
            this.SOH_Text = new serial_cmd.HexNumberTextBox(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号";
            // 
            // PortName
            // 
            this.PortName.FormattingEnabled = true;
            this.PortName.Location = new System.Drawing.Point(60, 11);
            this.PortName.Name = "PortName";
            this.PortName.Size = new System.Drawing.Size(86, 25);
            this.PortName.TabIndex = 1;
            this.PortName.SelectedIndexChanged += new System.EventHandler(this.PortName_SelectedIndexChanged);
            this.PortName.Click += new System.EventHandler(this.PortName_Click);
            // 
            // BaudRate
            // 
            this.BaudRate.FormattingEnabled = true;
            this.BaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000",
            "230400",
            "256000",
            "460800",
            "500000",
            "512000",
            "600000",
            "750000",
            "921600"});
            this.BaudRate.Location = new System.Drawing.Point(210, 11);
            this.BaudRate.Name = "BaudRate";
            this.BaudRate.Size = new System.Drawing.Size(86, 25);
            this.BaudRate.TabIndex = 3;
            this.BaudRate.SelectedIndexChanged += new System.EventHandler(this.BaudRate_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率";
            // 
            // But_OpenPort
            // 
            this.But_OpenPort.Location = new System.Drawing.Point(351, 44);
            this.But_OpenPort.Name = "But_OpenPort";
            this.But_OpenPort.Size = new System.Drawing.Size(105, 23);
            this.But_OpenPort.TabIndex = 4;
            this.But_OpenPort.Text = "打开串口";
            this.But_OpenPort.UseVisualStyleBackColor = true;
            this.But_OpenPort.Click += new System.EventHandler(this.But_OpenPort_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.state_Lable});
            this.statusStrip1.Location = new System.Drawing.Point(0, 512);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // state_Lable
            // 
            this.state_Lable.Name = "state_Lable";
            this.state_Lable.Size = new System.Drawing.Size(131, 17);
            this.state_Lable.Text = "toolStripStatusLabel1";
            // 
            // Com_DataBit
            // 
            this.Com_DataBit.FormattingEnabled = true;
            this.Com_DataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.Com_DataBit.Location = new System.Drawing.Point(370, 11);
            this.Com_DataBit.Name = "Com_DataBit";
            this.Com_DataBit.Size = new System.Drawing.Size(86, 25);
            this.Com_DataBit.TabIndex = 7;
            this.Com_DataBit.SelectedIndexChanged += new System.EventHandler(this.Com_DataBit_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "数据位";
            // 
            // Com_CheckBit
            // 
            this.Com_CheckBit.FormattingEnabled = true;
            this.Com_CheckBit.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.Com_CheckBit.Location = new System.Drawing.Point(210, 40);
            this.Com_CheckBit.Name = "Com_CheckBit";
            this.Com_CheckBit.Size = new System.Drawing.Size(86, 25);
            this.Com_CheckBit.TabIndex = 11;
            this.Com_CheckBit.SelectedIndexChanged += new System.EventHandler(this.Com_CheckBit_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "校验位";
            // 
            // Com_StopBit
            // 
            this.Com_StopBit.FormattingEnabled = true;
            this.Com_StopBit.Items.AddRange(new object[] {
            "1",
            "2"});
            this.Com_StopBit.Location = new System.Drawing.Point(60, 40);
            this.Com_StopBit.Name = "Com_StopBit";
            this.Com_StopBit.Size = new System.Drawing.Size(86, 25);
            this.Com_StopBit.TabIndex = 9;
            this.Com_StopBit.SelectedIndexChanged += new System.EventHandler(this.Com_StopBit_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "停止位";
            // 
            // But_Inventory_Device
            // 
            this.But_Inventory_Device.Location = new System.Drawing.Point(15, 85);
            this.But_Inventory_Device.Name = "But_Inventory_Device";
            this.But_Inventory_Device.Size = new System.Drawing.Size(95, 23);
            this.But_Inventory_Device.TabIndex = 12;
            this.But_Inventory_Device.Text = "盘点设备";
            this.But_Inventory_Device.UseVisualStyleBackColor = true;
            this.But_Inventory_Device.Click += new System.EventHandler(this.But_Inventory_Device_Click);
            // 
            // Device_List
            // 
            this.Device_List.Location = new System.Drawing.Point(125, 85);
            this.Device_List.Multiline = true;
            this.Device_List.Name = "Device_List";
            this.Device_List.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Device_List.Size = new System.Drawing.Size(363, 76);
            this.Device_List.TabIndex = 13;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "SOH";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(106, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Version";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(274, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "Address";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(191, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Length";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(540, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "CRC";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(35, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 17);
            this.label11.TabIndex = 20;
            this.label11.Text = "PUD";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(456, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 17);
            this.label12.TabIndex = 19;
            this.label12.Text = "CMD";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(358, 186);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 17);
            this.label13.TabIndex = 18;
            this.label13.Text = "Packet ID";
            // 
            // PUD_Text
            // 
            this.PUD_Text.Location = new System.Drawing.Point(25, 262);
            this.PUD_Text.Name = "PUD_Text";
            this.PUD_Text.Size = new System.Drawing.Size(558, 24);
            this.PUD_Text.TabIndex = 29;
            this.PUD_Text.TextChanged += new System.EventHandler(this.PUD_Text_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(277, 205);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(54, 25);
            this.comboBox1.TabIndex = 37;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(361, 206);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 24);
            this.numericUpDown1.TabIndex = 38;
            // 
            // Make_Data
            // 
            this.Make_Data.Location = new System.Drawing.Point(25, 317);
            this.Make_Data.Name = "Make_Data";
            this.Make_Data.Size = new System.Drawing.Size(105, 23);
            this.Make_Data.TabIndex = 39;
            this.Make_Data.Text = "数据组包";
            this.Make_Data.UseVisualStyleBackColor = true;
            // 
            // Send_Data
            // 
            this.Send_Data.Location = new System.Drawing.Point(166, 317);
            this.Send_Data.Name = "Send_Data";
            this.Send_Data.Size = new System.Drawing.Size(105, 23);
            this.Send_Data.TabIndex = 40;
            this.Send_Data.Text = "发送数据";
            this.Send_Data.UseVisualStyleBackColor = true;
            // 
            // CRC_Text
            // 
            this.CRC_Text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CRC_Text.Location = new System.Drawing.Point(529, 206);
            this.CRC_Text.MaxLength = 4;
            this.CRC_Text.Name = "CRC_Text";
            this.CRC_Text.ReadOnly = true;
            this.CRC_Text.Size = new System.Drawing.Size(54, 24);
            this.CRC_Text.TabIndex = 36;
            this.CRC_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CMD_Text
            // 
            this.CMD_Text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CMD_Text.Location = new System.Drawing.Point(445, 206);
            this.CMD_Text.MaxLength = 4;
            this.CMD_Text.Name = "CMD_Text";
            this.CMD_Text.Size = new System.Drawing.Size(54, 24);
            this.CMD_Text.TabIndex = 35;
            this.CMD_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Len_Text
            // 
            this.Len_Text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Len_Text.Location = new System.Drawing.Point(193, 206);
            this.Len_Text.MaxLength = 4;
            this.Len_Text.Name = "Len_Text";
            this.Len_Text.ReadOnly = true;
            this.Len_Text.Size = new System.Drawing.Size(54, 24);
            this.Len_Text.TabIndex = 32;
            this.Len_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VER_Text
            // 
            this.VER_Text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.VER_Text.Location = new System.Drawing.Point(109, 206);
            this.VER_Text.MaxLength = 4;
            this.VER_Text.Name = "VER_Text";
            this.VER_Text.Size = new System.Drawing.Size(54, 24);
            this.VER_Text.TabIndex = 31;
            this.VER_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SOH_Text
            // 
            this.SOH_Text.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.SOH_Text.Location = new System.Drawing.Point(25, 206);
            this.SOH_Text.MaxLength = 4;
            this.SOH_Text.Name = "SOH_Text";
            this.SOH_Text.Size = new System.Drawing.Size(54, 24);
            this.SOH_Text.TabIndex = 30;
            this.SOH_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Serial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(834, 534);
            this.Controls.Add(this.Send_Data);
            this.Controls.Add(this.Make_Data);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.CRC_Text);
            this.Controls.Add(this.CMD_Text);
            this.Controls.Add(this.Len_Text);
            this.Controls.Add(this.VER_Text);
            this.Controls.Add(this.SOH_Text);
            this.Controls.Add(this.PUD_Text);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Device_List);
            this.Controls.Add(this.But_Inventory_Device);
            this.Controls.Add(this.Com_CheckBit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Com_StopBit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Com_DataBit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.But_OpenPort);
            this.Controls.Add(this.BaudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Serial";
            this.ShowIcon = false;
            this.Text = "设备测试";
            this.Load += new System.EventHandler(this.Serial_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PortName;
        private System.Windows.Forms.ComboBox BaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button But_OpenPort;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel state_Lable;
        private System.Windows.Forms.ComboBox Com_DataBit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Com_CheckBit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Com_StopBit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button But_Inventory_Device;
        private System.Windows.Forms.TextBox Device_List;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox PUD_Text;
        private HexNumberTextBox SOH_Text;
        private HexNumberTextBox VER_Text;
        private HexNumberTextBox Len_Text;
        private HexNumberTextBox CMD_Text;
        private HexNumberTextBox CRC_Text;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button Make_Data;
        private System.Windows.Forms.Button Send_Data;
    }
}

