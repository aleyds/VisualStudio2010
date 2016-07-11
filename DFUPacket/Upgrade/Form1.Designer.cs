namespace Upgrade
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sLockType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.UpgradeType = new System.Windows.Forms.ComboBox();
            this.HardwareVersion = new System.Windows.Forms.TextBox();
            this.SoftwareVersion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LockMAC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.endSerial = new System.Windows.Forms.TextBox();
            this.startSerial = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EndDatePacker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.StartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.LoadFileText = new System.Windows.Forms.TextBox();
            this.SaveFileText = new System.Windows.Forms.TextBox();
            this.loadUpgradeFile = new System.Windows.Forms.OpenFileDialog();
            this.saveUpgradeFile = new System.Windows.Forms.SaveFileDialog();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.FileInfoText = new System.Windows.Forms.RichTextBox();
            this.PacketData = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.MCUComboBox = new System.Windows.Forms.ComboBox();
            this.FingerComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.PasswordComboBox = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.WifiComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.RFIDComboBox = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.HardwareForm = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AlarmCheckBox = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sLockType);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.UpgradeType);
            this.groupBox1.Controls.Add(this.HardwareVersion);
            this.groupBox1.Controls.Add(this.SoftwareVersion);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.LockMAC);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.endSerial);
            this.groupBox1.Controls.Add(this.startSerial);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.EndDatePacker);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.StartDatePicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 387);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "升级属性";
            // 
            // sLockType
            // 
            this.sLockType.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.sLockType.DisplayMember = "0";
            this.sLockType.FormattingEnabled = true;
            this.sLockType.Items.AddRange(new object[] {
            "sLock-1",
            "sLock-2",
            "sLock-3",
            "sLock-3FC",
            "sLock-3M",
            "sLock-1S"});
            this.sLockType.Location = new System.Drawing.Point(89, 214);
            this.sLockType.Name = "sLockType";
            this.sLockType.Size = new System.Drawing.Size(174, 20);
            this.sLockType.TabIndex = 18;
            this.sLockType.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "锁类型";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(175, 354);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 29);
            this.button3.TabIndex = 16;
            this.button3.Text = "打包";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnExecute);
            // 
            // UpgradeType
            // 
            this.UpgradeType.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.UpgradeType.DisplayMember = "0";
            this.UpgradeType.FormattingEnabled = true;
            this.UpgradeType.Items.AddRange(new object[] {
            "普通升级",
            "强制升级",
            "范围升级",
            "单用户升级"});
            this.UpgradeType.Location = new System.Drawing.Point(89, 251);
            this.UpgradeType.Name = "UpgradeType";
            this.UpgradeType.Size = new System.Drawing.Size(174, 20);
            this.UpgradeType.TabIndex = 16;
            this.UpgradeType.TabStop = false;
            // 
            // HardwareVersion
            // 
            this.HardwareVersion.Location = new System.Drawing.Point(89, 329);
            this.HardwareVersion.Name = "HardwareVersion";
            this.HardwareVersion.Size = new System.Drawing.Size(174, 21);
            this.HardwareVersion.TabIndex = 15;
            // 
            // SoftwareVersion
            // 
            this.SoftwareVersion.Location = new System.Drawing.Point(89, 289);
            this.SoftwareVersion.Name = "SoftwareVersion";
            this.SoftwareVersion.Size = new System.Drawing.Size(174, 21);
            this.SoftwareVersion.TabIndex = 14;
            this.SoftwareVersion.Text = "0x00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 333);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "硬件版本号:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 294);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "软件版本号:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "升级类型:";
            // 
            // LockMAC
            // 
            this.LockMAC.Location = new System.Drawing.Point(89, 179);
            this.LockMAC.Name = "LockMAC";
            this.LockMAC.Size = new System.Drawing.Size(174, 21);
            this.LockMAC.TabIndex = 9;
            this.LockMAC.Text = "FF:FF:FF:FF:FF:FF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "MAC:";
            // 
            // endSerial
            // 
            this.endSerial.Location = new System.Drawing.Point(89, 140);
            this.endSerial.Name = "endSerial";
            this.endSerial.Size = new System.Drawing.Size(174, 21);
            this.endSerial.TabIndex = 7;
            this.endSerial.Text = "FF:FF:FF:FF:FF:FF";
            // 
            // startSerial
            // 
            this.startSerial.Location = new System.Drawing.Point(89, 101);
            this.startSerial.Name = "startSerial";
            this.startSerial.Size = new System.Drawing.Size(174, 21);
            this.startSerial.TabIndex = 6;
            this.startSerial.Text = "FF:FF:FF:FF:FF:FF";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "结束序列号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "起始序列号:";
            // 
            // EndDatePacker
            // 
            this.EndDatePacker.Location = new System.Drawing.Point(89, 57);
            this.EndDatePacker.Name = "EndDatePacker";
            this.EndDatePacker.Size = new System.Drawing.Size(174, 21);
            this.EndDatePacker.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束日期:";
            // 
            // StartDatePicker
            // 
            this.StartDatePicker.Location = new System.Drawing.Point(89, 28);
            this.StartDatePicker.Name = "StartDatePicker";
            this.StartDatePicker.Size = new System.Drawing.Size(174, 21);
            this.StartDatePicker.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始日期:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(666, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "加载";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnLoadFile);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(666, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnPackFile);
            // 
            // LoadFileText
            // 
            this.LoadFileText.Location = new System.Drawing.Point(308, 61);
            this.LoadFileText.Name = "LoadFileText";
            this.LoadFileText.ReadOnly = true;
            this.LoadFileText.Size = new System.Drawing.Size(352, 21);
            this.LoadFileText.TabIndex = 8;
            // 
            // SaveFileText
            // 
            this.SaveFileText.Location = new System.Drawing.Point(308, 112);
            this.SaveFileText.Name = "SaveFileText";
            this.SaveFileText.Size = new System.Drawing.Size(352, 21);
            this.SaveFileText.TabIndex = 9;
            this.SaveFileText.Text = "firmware.m2";
            // 
            // loadUpgradeFile
            // 
            this.loadUpgradeFile.FileName = "Upgrade.Hex";
            this.loadUpgradeFile.Filter = "All File(*.*)|*.*|Hex(*.Hex)|*.Hex|M2 File(*.m2)|*.m2|Bin File(*.bin)|*.bin";
            this.loadUpgradeFile.FilterIndex = 4;
            // 
            // saveUpgradeFile
            // 
            this.saveUpgradeFile.Filter = "All File(*.*)|*.*|M2 File(*.m2)|*.m2";
            this.saveUpgradeFile.FilterIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(306, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "文件信息";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(306, 291);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "打包数据";
            // 
            // FileInfoText
            // 
            this.FileInfoText.Location = new System.Drawing.Point(308, 172);
            this.FileInfoText.Name = "FileInfoText";
            this.FileInfoText.ReadOnly = true;
            this.FileInfoText.Size = new System.Drawing.Size(422, 110);
            this.FileInfoText.TabIndex = 14;
            this.FileInfoText.Text = "";
            // 
            // PacketData
            // 
            this.PacketData.Location = new System.Drawing.Point(308, 306);
            this.PacketData.Name = "PacketData";
            this.PacketData.ReadOnly = true;
            this.PacketData.Size = new System.Drawing.Size(422, 110);
            this.PacketData.TabIndex = 15;
            this.PacketData.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "主芯片";
            // 
            // MCUComboBox
            // 
            this.MCUComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MCUComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "nrf51822 AA",
            "nrf51822 AC"});
            this.MCUComboBox.DisplayMember = "0";
            this.MCUComboBox.FormattingEnabled = true;
            this.MCUComboBox.Items.AddRange(new object[] {
            "nrf51822 AA",
            "nrf51822 AC"});
            this.MCUComboBox.Location = new System.Drawing.Point(65, 22);
            this.MCUComboBox.Name = "MCUComboBox";
            this.MCUComboBox.Size = new System.Drawing.Size(174, 20);
            this.MCUComboBox.TabIndex = 19;
            this.MCUComboBox.TabStop = false;
            // 
            // FingerComboBox
            // 
            this.FingerComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.FingerComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "nrf51822 AA",
            "nrf51822 AC"});
            this.FingerComboBox.DisplayMember = "0";
            this.FingerComboBox.FormattingEnabled = true;
            this.FingerComboBox.Items.AddRange(new object[] {
            "无",
            "艾迪数通",
            "霸凌"});
            this.FingerComboBox.Location = new System.Drawing.Point(65, 48);
            this.FingerComboBox.Name = "FingerComboBox";
            this.FingerComboBox.Size = new System.Drawing.Size(174, 20);
            this.FingerComboBox.TabIndex = 21;
            this.FingerComboBox.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "指纹模块";
            // 
            // PasswordComboBox
            // 
            this.PasswordComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.PasswordComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "nrf51822 AA",
            "nrf51822 AC"});
            this.PasswordComboBox.DisplayMember = "0";
            this.PasswordComboBox.FormattingEnabled = true;
            this.PasswordComboBox.Items.AddRange(new object[] {
            "无",
            "TTY6952"});
            this.PasswordComboBox.Location = new System.Drawing.Point(65, 74);
            this.PasswordComboBox.Name = "PasswordComboBox";
            this.PasswordComboBox.Size = new System.Drawing.Size(174, 20);
            this.PasswordComboBox.TabIndex = 23;
            this.PasswordComboBox.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 22;
            this.label14.Text = "密码模块";
            // 
            // WifiComboBox
            // 
            this.WifiComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.WifiComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "nrf51822 AA",
            "nrf51822 AC"});
            this.WifiComboBox.DisplayMember = "0";
            this.WifiComboBox.FormattingEnabled = true;
            this.WifiComboBox.Items.AddRange(new object[] {
            "无",
            "CC3100"});
            this.WifiComboBox.Location = new System.Drawing.Point(65, 99);
            this.WifiComboBox.Name = "WifiComboBox";
            this.WifiComboBox.Size = new System.Drawing.Size(174, 20);
            this.WifiComboBox.TabIndex = 25;
            this.WifiComboBox.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 24;
            this.label15.Text = "wifi模块";
            // 
            // RFIDComboBox
            // 
            this.RFIDComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.RFIDComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "nrf51822 AA",
            "nrf51822 AC"});
            this.RFIDComboBox.DisplayMember = "0";
            this.RFIDComboBox.FormattingEnabled = true;
            this.RFIDComboBox.Items.AddRange(new object[] {
            "无",
            "RC522",
            "M401C"});
            this.RFIDComboBox.Location = new System.Drawing.Point(65, 125);
            this.RFIDComboBox.Name = "RFIDComboBox";
            this.RFIDComboBox.Size = new System.Drawing.Size(174, 20);
            this.RFIDComboBox.TabIndex = 27;
            this.RFIDComboBox.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 128);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 26;
            this.label16.Text = "RFID";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 153);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 28;
            this.label17.Text = "防撬报警";
            // 
            // HardwareForm
            // 
            this.HardwareForm.Location = new System.Drawing.Point(162, 182);
            this.HardwareForm.Name = "HardwareForm";
            this.HardwareForm.Size = new System.Drawing.Size(77, 29);
            this.HardwareForm.TabIndex = 30;
            this.HardwareForm.Text = "版本号生成";
            this.HardwareForm.UseVisualStyleBackColor = true;
            this.HardwareForm.Click += new System.EventHandler(this.OnHardwareClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AlarmCheckBox);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.HardwareForm);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.MCUComboBox);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.FingerComboBox);
            this.groupBox2.Controls.Add(this.RFIDComboBox);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.PasswordComboBox);
            this.groupBox2.Controls.Add(this.WifiComboBox);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(762, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(313, 225);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "硬件版本号";
            // 
            // AlarmCheckBox
            // 
            this.AlarmCheckBox.AutoSize = true;
            this.AlarmCheckBox.Location = new System.Drawing.Point(67, 154);
            this.AlarmCheckBox.Name = "AlarmCheckBox";
            this.AlarmCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AlarmCheckBox.TabIndex = 31;
            this.AlarmCheckBox.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(924, 339);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 57);
            this.button4.TabIndex = 32;
            this.button4.Text = "上传服务器";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnUploadFile);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 429);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.PacketData);
            this.Controls.Add(this.FileInfoText);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.SaveFileText);
            this.Controls.Add(this.LoadFileText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Hex打包";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker StartDatePicker;
        private System.Windows.Forms.TextBox LockMAC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox endSerial;
        private System.Windows.Forms.TextBox startSerial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker EndDatePacker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HardwareVersion;
        private System.Windows.Forms.TextBox SoftwareVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox LoadFileText;
        private System.Windows.Forms.TextBox SaveFileText;
        private System.Windows.Forms.ComboBox UpgradeType;
        private System.Windows.Forms.OpenFileDialog loadUpgradeFile;
        private System.Windows.Forms.SaveFileDialog saveUpgradeFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox FileInfoText;
        private System.Windows.Forms.RichTextBox PacketData;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox sLockType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox MCUComboBox;
        private System.Windows.Forms.ComboBox FingerComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox PasswordComboBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox WifiComboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox RFIDComboBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button HardwareForm;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox AlarmCheckBox;
        private System.Windows.Forms.Button button4;
    }
}

