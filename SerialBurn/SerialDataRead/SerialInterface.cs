using System;
using System.IO;
using System.IO.Ports;
using System.Text;

using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SerialDataRead
{
    public partial class WriteData : Form
    {
        SerialPort serialPortA;
        Thread readTheard;
        
        int CmdSendTime = 500;
        private int ActonTime = 0;
        public WriteData()
        {
            InitializeComponent();
            SetSerialCom();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void UserConfigParam()
        {
            if ((serialPortA != null) && (serialPortA.IsOpen))
            {
                return;
            }
            if (serialPortA == null)
            {
                serialPortA = new SerialPort(SerialComcomboBox.Text);
            }
            
            serialPortA.BaudRate = int.Parse(BaudRatecomboBox.Text);
            switch (ParitycomboBox.SelectedIndex)
            {
                case 0:
                    serialPortA.Parity = Parity.None;
                    break;
                case 1:
                    serialPortA.Parity = Parity.Odd;
                    break;
                case 2:
                    serialPortA.Parity = Parity.Even;
                    break;
                default:
                    serialPortA.Parity = Parity.None;
                    break;

            }
            switch (StopBitscomboBox.SelectedIndex)
            {
                case 0:
                    serialPortA.StopBits = StopBits.One;
                    break;
                case 1:
                    serialPortA.StopBits = StopBits.Two;
                    break;
                default:
                    serialPortA.StopBits = StopBits.One;
                    break;

            }

            serialPortA.DataBits = int.Parse(DataBitscomboBox.Text);
            switch (HandshakecomboBox.SelectedIndex)
            {
                case 0:
                    serialPortA.Handshake = Handshake.None;//控制协议
                    break;
                case 1:
                    serialPortA.Handshake = Handshake.RequestToSend;//控制协议
                    break;
                default:
                    serialPortA.Handshake = Handshake.None;//控制协议
                    break;
            }

        }

        private void SaveFileClick(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLogPath.Text = saveFileDialog.FileName;
                
            }
        }

        private void SendCmdClick(object sender, EventArgs e)
        {
            string SendD = SendCmdTxt.Text;
            if (SendD.Equals(""))
            {
                MessageBox.Show("指令为空");
                return;
            }
           // this.MessageRichTextBox.AppendText(SendD+"\n");
            SendSerialData(SendD);
        }

        private void OpenFileClick(object sender, EventArgs e)
        {
            openFileDialog.Filter = "smartBASIC文件(*.uwc)|*.uwc|所有文件(*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFileTxt.Text = openFileDialog.FileName;

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public int SerialPortOpen()
        {
            if (serialPortA == null)
            {
                MessageBox.Show("串口没有配置");
                return -1;
            }
            if (serialPortA.IsOpen)
            {
                SetRichBox("串口已经打开\n");
                return 0;
            }
            try
            {
                serialPortA.Open();
            }
            catch { }
            if (serialPortA.IsOpen)
            {
                return 0;
            }
            else
            {
                Console.WriteLine("The Port is Not Open!!!");
                return -1;
            }
        }// end SerialPortOpen

        public int SerialPortClose()
        {
            if (serialPortA == null)
            {
                return -2;
            }
            if (serialPortA.IsOpen)
            {
                serialPortA.Close();
                if (serialPortA.IsOpen)
                {
                    Console.WriteLine("The Port Close Faild!!!");
                    return -1;
                }
                return 0;
            }
            else
            {
                return -2;
            }
            
            return 0;
        }//end SerialPortClose 

        private void SetSerialCom()
        {
            string[] PortAll = SerialPort.GetPortNames();
           // string str = "";
            int i = 0;
            foreach  (string str in PortAll)
            {
                if (i == 0)
                {
                    SerialComcomboBox.Text = str;
                    i++;
                }
                SerialComcomboBox.Items.Add(str);
            }
            

        }

        private Boolean serialOpen()
        {
            UserConfigParam();
            if (SerialPortOpen() != 0)
            {

                SetRichBox("串口打开失败\n");
                return false;
            }
            else
            {

                SetRichBox("串口打开成功\n");
                if ((readTheard == null) || (!readTheard.IsAlive))
                {
                    readTheard = new Thread(new ThreadStart(ThreadProc));
                    readTheard.Start();
                }
                return true;
            }
        }


        private void SerialConnectClick(object sender, EventArgs e)
        {
            int ret = 0;
            serialOpen();
            
        }

        public void SendSerialData(string Data)
        {
           // Console.WriteLine(Data);
           // this.MessageRichTextBox.AppendText(Data + "\n");
            if (serialPortA == null)
            {
                SetMessage("串口未初始化\n");
                return ;
            }
            if (!serialPortA.IsOpen)
            {
                SetMessage("串口未连接\n");
                return;
            }
            SetMessage(Data + "\n");
            Data += "\r\n\r\n";

            byte[] WriteBuffer = Encoding.ASCII.GetBytes(Data);
            serialPortA.Write(WriteBuffer, 0, WriteBuffer.Length);

        }

        public string ReadSerialData()
        {
            if (serialPortA == null)
            {
                SetMessage("串口未初始化\n");
                return "";
            }
            if (!serialPortA.IsOpen)
            {
                SetMessage("串口未连接\n");
                return "";
            }
            string back = serialPortA.ReadExisting();
            
            //FeedBackData += back;
            Console.WriteLine(back);
            return back.ToString();
        }

        public void ThreadProc()
        {

            while(true)
            {
                string backData = ReadSerialData();
                if (!backData.Equals("") && (!backData.Equals("\n00\r")) && (!backData.Equals("\n00\r\n00\r")) && (!backData.Equals("\n00\r\n00"))
                    && (!backData.Equals("\r\n00\r")) && (!backData.Equals("\n00\r\n")) && (!backData.Equals("\n00\r\n0")) && (!backData.Equals("0\r"))
                    && (!backData.Equals("00\r\n00\r")) && (!backData.Equals("\n0")) && (!backData.Equals("\n00")) && (!backData.Equals("0\r\n00\r"))
                    && (!backData.Equals("00\r")) && (!backData.Equals("\r")) && (!backData.Equals("\n")))
                {
                   
                        SetMessage(backData);
                    
                    
                }
                
                Thread.Sleep(10);

            }
            SerialPortClose();
        }

        public delegate void MyInvoke(string str);
        public void SetMessage(string Data)
        {
            //MessageRichTextBox.Text += Data;
            if (MessageRichTextBox.InvokeRequired)
            {
                MyInvoke _myInvoke = new MyInvoke(SetMessage);
                this.Invoke(_myInvoke, new object[] { Data });
            }
            else
            {
                SetRichBox(Data);
            }
            
        }

        private void SetRichBox(string Data)
        {
            this.MessageRichTextBox.AppendText(Data);
            this.MessageRichTextBox.Focus();
            this.MessageRichTextBox.Select(this.MessageRichTextBox.TextLength, 0);
            this.MessageRichTextBox.ScrollToCaret();

        }

        private void MessageClear()
        {
            MessageRichTextBox.Clear();
            
        }

        private void SerialDisCountentClick(object sender, EventArgs e)
        {
            int ret = 0;
            if (readTheard != null)
            {
                readTheard.Abort();
            }
            
            ret = SerialPortClose();
            if (ret == -2)
            {
                SetRichBox("串口未打开\n");
            }
            else if (ret == -1)
            {
                SetRichBox("串口关闭失败\n");
            }
            else
            {
                SetRichBox("串口已关闭\n");
            }
        }

        

        

        public void WriteAutoProc()
        {
            File_Engine mfile = new File_Engine();
            string filePath = OpenFileTxt.Text;
            mfile.smartBASICWriteAutorun(filePath, serialPortA, this);
            WriteDataTimer.Stop();
    
        }
        private string BurnPath;
        private void FileBurnClick(object sender, EventArgs e)
        {
            string filePath = OpenFileTxt.Text;
            ActonTime = 0;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("路径下不存在"+filePath+"烧写文件");
                return;
            }

            if ((null != serialPortA) && (serialPortA.IsOpen))
            {
                // mfile.smartBASICWrite("temp.uwc", serialPortA);
                new Thread(new ThreadStart(WriteAutoProc)).Start();
                WriteDataTimer.Start();
            }
            else
            {
                SetRichBox("串口未打开不能烧写\n");
                DialogResult result = MessageBox.Show("串口未打开是否开启？", "开启串口", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    serialOpen();
                }
            }
            
        }

        private void OpenLogTxtClick(object sender, EventArgs e)
        {
            string Path = SaveLogPath.Text;
            if (Path.Equals(""))
            {
                MessageBox.Show("Log文件为空");
                return;
            }
            FileStream file = new FileStream(Path,FileMode.Create);
      
        }

        private void OverLogTxtClick(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            if (PasswordChecked.Checked)
            {
                PasswordOrigin.Enabled = false;
            }
            else
            {
                PasswordOrigin.Enabled = true;
            }
            
        }

        public string GetRandPwd()
        {
            int min = 10000000;
            int max = 99999999;
            Random ran = new Random();
            int RandKey = ran.Next(min, max);
           
            return RandKey.ToString();
        }

        private string getPassword()
        {
            string password = "";
            if (PasswordChecked.Checked)
            {
                password = GetRandPwd();
                PasswordOrigin.Text = password;
                return password;
            }
            else
            {
                password = PasswordOrigin.Text;
                return password;
            }
        }

        private void WriteProc()
        {
            File_Engine mfile = new File_Engine();
            if (!File.Exists("temp.uwc"))
            {
                MessageBox.Show("没有生成temp.uwc文件");
                return;
            }
            mfile.smartBASICWrite("temp.uwc", serialPortA,this);
            WriteDataTimer.Stop();
        }

        private Boolean isChinese(string str)
        {
            Regex reg = new Regex("[\u4e00-\u9fa5]");
            Match mh = reg.Match(str);
            return mh.Success;
        }

        private Boolean isNotDigital(string str)
        {
            Regex reg = new Regex("[^0-9]");
            Match mh = reg.Match(str);
            return mh.Success;
        }

        private Boolean checkInput()
        {

            string pwd = PasswordOrigin.Text;
            string Interative = PasswordInterative.Text;
            string PwdLenght = PasswordLenght.Text;
            if (pwd.Equals(""))
            {
                MessageBox.Show("输入密码为空");
                return false;
            }
            if (pwd.Length < 8)
            {
                MessageBox.Show("密码长度小于8位");
                return false;
            }
            if (isChinese(pwd))
            {
                MessageBox.Show("输入密码有中文，暂时不支持中文密码");
                return false;
            }

            if (isNotDigital(PwdLenght))
            {
                MessageBox.Show("长度应为数字");
                return false;
            }
           
            if (isNotDigital(Interative))
            {
                MessageBox.Show("迭代中应为数字");
                return false;
            }
            string xcomName = XCompFileName.Text;
            string OriginFile = OriginFileName.Text;
            if (!File.Exists(xcomName))
            {
                MessageBox.Show("该路径下的编译链不存在");
                return false;
            }
            if (!File.Exists(OriginFile))
            {
                MessageBox.Show("该路径下的源文件不存在");
                return false;
            }

            return true;
        }

        private void OnClickPasswordWriteing(object sender, EventArgs e)
        {
            PBKDF2_Engine mpbkdf2 = new PBKDF2_Engine();
            File_Engine mfile = new File_Engine();
            ActonTime = 0;
           
            string pwd = getPassword() ;
            if (!checkInput())
            {
                return;
            }
            int pwdLen =  System.Int32.Parse(PasswordLenght.Text);
            int BitLen =  System.Int32.Parse(PasswordBitLenght.Text);
            int Iterative = System.Int32.Parse(PasswordInterative.Text);
            string outdata = mpbkdf2.PBKDF2Gen(pwd, Iterative,pwdLen,BitLen);
            string xcomName = XCompFileName.Text;
            string OriginFile = OriginFileName.Text;
           // string CreateFile = CreateFileName.Text;
            string code = "\"" + pwd + " " + outdata + "\"";
            Passwordciphertext.Text = outdata;
            mfile.SavePasswordFile(OriginFile, "temp.sb", code);
            mfile.XCompileFile(xcomName, "temp.sb");
            if ((null != serialPortA)&&(serialPortA.IsOpen))
            {
               // mfile.smartBASICWrite("temp.uwc", serialPortA);
                new Thread(new ThreadStart(WriteProc)).Start();
                WriteDataTimer.Start();
            }
            else
            {
                SetRichBox("串口未打开不能烧写\n");
                DialogResult result = MessageBox.Show("串口未打开是否开启？","开启串口", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    serialOpen();
                }
                
            }
           

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void OnPasswordTxtChange(object sender, EventArgs e)
        {
            PasswordTxtLen.Text = "长度:" + PasswordOrigin.Text.Length;
        }

        private void OnClickOriginFile(object sender, EventArgs e)
        {
            openOriginFileDialog.Filter = "smartBASIC源文件(*.sb)|*.sb|所有文件(*.*)|*.*";

            if (openOriginFileDialog.ShowDialog() == DialogResult.OK)
            {
                OriginFileName.Text = openOriginFileDialog.FileName;

            }
        }

        private void OnClickCompileFile(object sender, EventArgs e)
        {
            openCompileFileDialog.Filter = "smartBASIC编译链(*.exe)|*.exe|所有文件(*.*)|*.*";

            if (openCompileFileDialog.ShowDialog() == DialogResult.OK)
            {
                XCompFileName.Text = openCompileFileDialog.FileName;

            }
        }

        private void OnFromLeave(object sender, EventArgs e)
        {
            if (readTheard != null)
            {
                readTheard.Abort();
            }
            SerialPortClose();
        }

        private void OnFromLoad(object sender, EventArgs e)
        {

            SerialPortClose();
        }

        private void OnFromClosed(object sender, FormClosedEventArgs e)
        {
            if (readTheard != null)
            {
                readTheard.Abort();
            }
            SerialPortClose();
        }

        private void OnClickClearMessage(object sender, EventArgs e)
        {
            MessageClear();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            ActonTime+=100;
            int ms = ActonTime % 1000;
            int s = (ActonTime / 1000) % 60;
            int m = ((ActonTime / 1000) / 60) % 60;
            int h = (ActonTime / (3600*1000));
            ActionTimeTxt.Text = "" + h+":"+m+":"+s+":"+ms;
        }

        private void PasswordWrite(object sender, EventArgs e)
        {

        }
    }
}
