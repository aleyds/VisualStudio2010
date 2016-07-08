using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using SpeechLib;
using System.Threading;
namespace WifiVideo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Boolean TorchEnable = false;
        private Boolean AlarmEnable = false;

        private Boolean FormKeyDown = false;
        private Boolean CustomSwitch = false;
        //语音识别部分
     /*   private SpeechLib.ISpeechRecoGrammar isrg;
        private SpeechLib.SpSharedRecoContextClass ssrContex = null;
        public delegate void StringEvent(string str);
        public StringEvent SetMessage;*/
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        static string FileName = Application.StartupPath + "\\Config.ini"; 
        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }

         string CameraIp = "";
         string PicturePath = "";
         string ControlIp = "192.168.1.1";
         string Port = "81";
         string CMD_Forward = "", CMD_Backward = "", CMD_TurnLeft = "", CMD_TurnRight = "", CMD_Stop = "", CMD_EngineUpRest="",CMD_EngineLeft="",CMD_EngineRight="",CMD_EngineDownRest="",CMD_EngineUp="",CMD_EngineDown="";
         string CMD_LeftForward = "", CMD_RightForward = "", CMD_LeftBackward = "", CMD_RightBackward = "";
         string CMD_TorchOn = "", CMD_TorchOff = "", CMD_AlarmOn = "", CMD_AlarmOff = "";
         string CMD_Custom = "", CMD_CustomOff = "";

        private void button1_Click(object sender, EventArgs e)
        {
                timer1.Enabled = true;
        }

        private void ThreadVedio()
        {
            Image netImage = Image.FromStream(WebRequest.Create(CameraIp).GetResponse().GetResponseStream());
            VedioPicture.Image = netImage;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VedioPicture.ImageLocation = CameraIp;// "http://192.168.1.1:8080/?action=snapshot";
            //Image netImage = Image.FromStream(WebRequest.Create(CameraIp).GetResponse().GetResponseStream());
            //VedioPicture.Image = netImage;
            //Thread myThread = new Thread(ThreadVedio);
           // myThread.Start();
        }

        private byte[] StringToByte(String Str)
        {
            int len = Str.Length;
            int i = 0;
            byte[] ByteArr = new byte[len / 2];

            for ( i = 0; i < len / 2; i++)
            {


                ByteArr[i] = (byte)Int32.Parse(Str.Substring((i * 2), 2), System.Globalization.NumberStyles.HexNumber); ;
            }
            return ByteArr;
        }

       
        private void ThreadFun(object ParObject)
        {
            string mStr = (String)ParObject;
            try
            {

                IPAddress ips = IPAddress.Parse(ControlIp.ToString());//("192.168.2.1");
                IPEndPoint ipe = new IPEndPoint(ips, Convert.ToInt32(Port.ToString()));//把ip和端口转化为IPEndPoint实例
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket

                c.Connect(ipe);//连接到服务器

                byte[] bs = StringToByte(mStr);
                // byte[] bs = Encoding.ASCII.GetBytes(data);  
                c.Send(bs, bs.Length, 0);//发送测试信息
                c.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void SendData(string data)
        {
        
            Thread myThread = new Thread(ThreadFun);
            myThread.Start(data);
            
        }

        private string ByteToString(byte myByte)
        {
            char[] HexChar = "0123456789ABCDEF".ToCharArray();
            string outString = HexChar[(myByte >> 4) & 0xf].ToString() + HexChar[(myByte) & 0xf].ToString();
            return outString;
        }

        private string SteerCommand(byte Channel, byte value)
        {
            string Cmd = "";
            Cmd = "FF01" + ByteToString(Channel) + ByteToString(value) + "FF";
            return Cmd;
        }

        private void btnEngineUp_Click(object sender, EventArgs e)
        {
            if (!SteerCheck1.Checked)
            {
                MessageBox.Show("请选择舵机1");
                return;
            }
            int SteerValue = SteerSlide1.Value;
            if (SteerValue >= SteerSlide1.Maximum)
            {
                return;
            }
            else if (SteerValue < 0)
            {
                SteerValue = SteerSlide1.Maximum;
            }
            else
            {
                SteerValue++; 
            }
            SteerSlide1.Value = SteerValue;
            string CMD_Steer1 = SteerCommand(1, (byte)SteerValue);
            SendData(CMD_Steer1);
        }

        private void btnEngineDown_Click(object sender, EventArgs e)
        {
            if (!SteerCheck1.Checked)
            {
                MessageBox.Show("请选择舵机1");
                return;
            }
            int SteerValue = SteerSlide1.Value;
            if (SteerValue > SteerSlide1.Maximum)
            {
                SteerValue = 0;
            }
            else if (SteerValue <= 0)
            {
                return;
            }
            else
            {
                SteerValue--;
            }
            SteerSlide1.Value = SteerValue;
            string CMD_Steer1 = SteerCommand(1, (byte)SteerValue);
            SendData(CMD_Steer1);
        }


        private void btnEngineLeft_Click(object sender, EventArgs e)
        {
            if (!SteerCheck2.Checked)
            {
                MessageBox.Show("请选择舵机2");
                return;
            }
            int SteerValue = SteerSlide2.Value;
            if (SteerValue >= SteerSlide2.Maximum)
            {
                return;
            }
            else if (SteerValue < 0)
            {
                SteerValue = SteerSlide2.Maximum;
            }
            else
            {
                SteerValue++;
            }
            SteerSlide2.Value = SteerValue;
            string CMD_Steer2 = SteerCommand(2, (byte)SteerValue);
             SendData(CMD_Steer2);
        }
        private void btnEngineRight_Click_1(object sender, EventArgs e)
        {
            if (!SteerCheck2.Checked)
            {
                MessageBox.Show("请选择舵机2");
                return;
            }
            int SteerValue = SteerSlide2.Value;
            if (SteerValue > SteerSlide2.Maximum)
            {
                SteerValue = 0;
            }
            else if (SteerValue <= 0)
            {
                return;
            }
            else
            {
                SteerValue--;
            }
            SteerSlide2.Value = SteerValue;
            string CMD_Steer2 = SteerCommand(2, (byte)SteerValue);
            SendData(CMD_Steer2);
        }

        private void btnEngineUpRest_Click(object sender, EventArgs e)
        {
            int SteerValue = 90;
            SteerSlide1.Value = SteerValue;
            string CMD_Steer = SteerCommand(1, (byte)SteerValue);
            SendData(CMD_Steer);
        }
        
        private void btnEngineDownRest_Click(object sender, EventArgs e)
        {
            int SteerValue = 90;
            SteerSlide2.Value = SteerValue;
            string CMD_Steer = SteerCommand(2, (byte)SteerValue);
            SendData(CMD_Steer);
        }
       
       
        private void button6_Click(object sender, EventArgs e)
        {
            Config cfg = new Config();
            cfg.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetIni();
            buttonForward.BackColor = Color.LightBlue;
            buttonBackward.BackColor = Color.LightBlue;
            buttonLeft.BackColor = Color.LightBlue;
            buttonRight.BackColor = Color.LightBlue;

            buttonLeftUP.BackColor = Color.LightBlue;
            buttonRightUP.BackColor = Color.LightBlue;
            buttonLeftDown.BackColor = Color.LightBlue;
            buttonRightDown.BackColor = Color.LightBlue;
            //buttonStop.BackColor = Color.LightBlue;
            btnEngineUpRest.BackColor = Color.LightBlue;
            btnEngineLeft.BackColor = Color.LightBlue;
            btnEngineRight.BackColor = Color.LightBlue;
            btnEngineDownRest.BackColor = Color.LightBlue;
            btnEngineUp.BackColor = Color.LightBlue;
            btnEngineDown.BackColor = Color.LightBlue;
            VedioButton.BackColor = Color.LightBlue;
            TorchButton.BackColor = Color.LightBlue;
            AlarmButton.BackColor = Color.LightBlue;
        }
        private void GetIni()
        {
            CameraIp = ReadIni("VideoUrl", "videoUrl", "");
            PicturePath = ReadIni("VideoUrl", "PicturePath", "");
            ControlIp = ReadIni("ControlUrl", "controlUrl", "");
            Port = ReadIni("ControlPort", "controlPort", "");
            //电机
            CMD_Forward = ReadIni("ControlCommand", "CMD_Forward", "");
            CMD_Backward = ReadIni("ControlCommand", "CMD_Backward", "");
            CMD_TurnLeft = ReadIni("ControlCommand", "CMD_TurnLeft", "");
            CMD_TurnRight = ReadIni("ControlCommand", "CMD_TurnRight", "");
            CMD_Stop = ReadIni("ControlCommand", "CMD_Stop", "");
            CMD_LeftForward = ReadIni("ControlCommand", "CMD_LeftForward", "");
            CMD_RightForward = ReadIni("ControlCommand", "CMD_RightForward", "");
            CMD_LeftBackward = ReadIni("ControlCommand", "CMD_LeftBackward", "");
            CMD_RightBackward = ReadIni("ControlCommand", "CMD_RightBackward", "");
            CMD_TorchOn = ReadIni("ControlCommand", "CMD_TorchOn", "");
            CMD_TorchOff = ReadIni("ControlCommand", "CMD_TorchOff", "");
            CMD_AlarmOn = ReadIni("ControlCommand", "CMD_AlarmOn", "");
            CMD_AlarmOff = ReadIni("ControlCommand", "CMD_AlarmOff", "");
            CMD_Custom = ReadIni("ControlCommand", "CMD_Custom", "");
            CMD_CustomOff = ReadIni("ControlCommand", "CMD_CustomOff", "");
            //舵机
            CMD_EngineUpRest = ReadIni("ControlCommand", "CMD_EngineUpRest", "");//上复位
            CMD_EngineLeft = ReadIni("ControlCommand", "CMD_EngineLeft", "");    //上左转
            CMD_EngineRight = ReadIni("ControlCommand", "CMD_EngineRight", "");  //上右转
            CMD_EngineDownRest = ReadIni("ControlCommand", "CMD_EngineDownRest", "");//下复位
            CMD_EngineUp = ReadIni("ControlCommand", "CMD_EngineUp", "");            //下，上转
            CMD_EngineDown = ReadIni("ControlCommand", "CMD_EngineDown", "");         //下，下转
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!FormKeyDown)
            {
                FormKeyDown = true;
            }
            else
            {
                return;
            }
            if (e.KeyCode == Keys.W)//前
            {
                buttonForward.BackColor = Color.DarkGray;

                //buttonForward.PerformClick();
                SendData(CMD_Forward);
              
            }
            else if (e.KeyCode == Keys.S)//后
            {
                buttonBackward.BackColor = Color.DarkGray;
                //buttonBackward.PerformClick();
                SendData(CMD_Backward);
               
            }
            else if (e.KeyCode == Keys.A)//左
            {
                buttonLeft.BackColor = Color.DarkGray;
                //buttonLeft.PerformClick();

                SendData(CMD_TurnLeft);
            }
            else if (e.KeyCode == Keys.D)//右
            {
                buttonRight.BackColor = Color.DarkGray;
                //buttonRight.PerformClick();
                SendData(CMD_TurnRight);
            }
            else if (e.KeyCode == Keys.Q)//左前
            {
                buttonLeftUP.BackColor = Color.DarkGray;
                //buttonLeftUP.PerformClick();
                SendData(CMD_LeftForward);
               
            }
            else if (e.KeyCode == Keys.E)//右前
            {
                buttonRightUP.BackColor = Color.DarkGray;
                //buttonRightUP.PerformClick();
                SendData(CMD_RightForward);

            }
            else if (e.KeyCode == Keys.Z)//左后
            {
                buttonLeftDown.BackColor = Color.DarkGray;
               // buttonLeftDown.PerformClick();
                SendData(CMD_LeftBackward);

            }
            else if (e.KeyCode == Keys.C)//右后
            {
                buttonRightDown.BackColor = Color.DarkGray;
                //buttonRightDown.PerformClick();
                SendData(CMD_RightBackward);

            }
            else if (e.KeyCode == Keys.I)//上
            {
                btnEngineUp.BackColor = Color.DarkGray;
                btnEngineUp.PerformClick();
            }
            else if (e.KeyCode == Keys.K)//下
            {
                btnEngineDown.BackColor = Color.DarkGray;
                btnEngineDown.PerformClick();
            }
            else if (e.KeyCode == Keys.J)//左
            {
                btnEngineLeft.BackColor = Color.DarkGray;
                btnEngineLeft.PerformClick();
            }
            else if (e.KeyCode == Keys.L)//右
            {
                btnEngineRight.BackColor = Color.DarkGray;
                btnEngineRight.PerformClick();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //buttonStop.PerformClick();
            buttonForward.BackColor = Color.LightBlue;
            buttonBackward.BackColor = Color.LightBlue;
            buttonLeft.BackColor = Color.LightBlue;
            buttonRight.BackColor = Color.LightBlue;

            buttonLeftUP.BackColor = Color.LightBlue;
            buttonRightUP.BackColor = Color.LightBlue;
            buttonLeftDown.BackColor = Color.LightBlue;
            buttonRightDown.BackColor = Color.LightBlue;
            //
          
            btnEngineUp.BackColor = Color.LightBlue;
            btnEngineDown.BackColor = Color.LightBlue;
            btnEngineLeft.BackColor = Color.LightBlue;
            btnEngineRight.BackColor = Color.LightBlue;
            FormKeyDown = false;

            if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D)
                || (e.KeyCode == Keys.Q) || (e.KeyCode == Keys.E) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.C))
            {
                SendData(CMD_Stop);
            }
        }



       /* private void button2_Click_1(object sender, EventArgs e)
        {
            InitVoice();
        }

        private void InitVoice()
        {
            ssrContex = new SpSharedRecoContextClass();
            isrg = ssrContex.CreateGrammar(1);
            SpeechLib._ISpeechRecoContextEvents_RecognitionEventHandler recHandle =
                 new _ISpeechRecoContextEvents_RecognitionEventHandler(ContexRecognition);
            ssrContex.Recognition += recHandle;
        }   
        public void BeginRec()
        {
            isrg.DictationSetState(SpeechRuleState.SGDSActive);
        }

        public void CloseRec()
        {
            isrg.DictationSetState(SpeechRuleState.SGDSInactive);
        }*/

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OnSettingClick(object sender, EventArgs e)
        {
            Config cfg = new Config();
            cfg.ShowDialog();
        }

        private void OnVideoClick(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                VedioLable.Text = "视频开";
            }
            else
            {
                timer1.Enabled = false;
                VedioLable.Text = "视频关";
            }
            
        }

        //电筒
        private void OnTorchClick(object sender, EventArgs e)
        {
            if (!TorchEnable)
            {
                SendData(CMD_TorchOn);
                TorchEnable = true;
                TorchLable.Text = "车灯开";
            }
            else 
            {
                SendData(CMD_TorchOff);
                TorchEnable = false;
                TorchLable.Text = "车灯关";
            }
            
        }

        //喇叭
        private void OnAlarmClick(object sender, EventArgs e)
        {
            if (!AlarmEnable)
            {
                SendData(CMD_AlarmOn);
                AlarmEnable = true;
                AlarmLable.Text = "喇叭开";
            }
            else 
            {
                SendData(CMD_AlarmOff);
                AlarmEnable = false;
                AlarmLable.Text = "喇叭关";
            }
        }

        private string SavePhotoName()
        {
            string Name = "Photo-"+DateTime.Now.ToString().Replace(" ",".").Replace("/","-").Replace(":","-")+"-Vedio.jpg";
            return Name;
        }

        //照相功能
        private void OnPhotoClick(object sender, EventArgs e)
        {
            string SaveName = "";
            string PathName = PicturePath.Replace("\0", "");
            if (Directory.Exists(PathName))
            {
                SaveName = PathName + "\\" + SavePhotoName();
            }
            else
            {
                if (!Directory.Exists("Picture"))
                {
                    Directory.CreateDirectory("Picture");
                }
                SaveName = "Picture\\"+ SavePhotoName();
            }
            
            Bitmap bit = new Bitmap(VedioPicture.Width, VedioPicture.Height);
            Graphics g = Graphics.FromImage(bit);
            VedioPicture.DrawToBitmap(bit, VedioPicture.ClientRectangle);
            bit.Save(SaveName , System.Drawing.Imaging.ImageFormat.Jpeg);
            bit.Dispose();
            MessageBox.Show( "文件名:" + SaveName,"保存成功");
        }

    

  
        private void OnCommandMouseUP(object sender, MouseEventArgs e)
        {
            SendData(CMD_Stop);
        }

        private void OnSteerCheck3(object sender, EventArgs e)
        {
            if (SteerCheck3.Checked)
            {
                int SteerValue = SteerSlide3.Value;
                string CMD_Steer = SteerCommand(3, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnSteerCheck4(object sender, EventArgs e)
        {
            if (SteerCheck4.Checked)
            {
                int SteerValue = SteerSlide4.Value;
                string CMD_Steer = SteerCommand(4, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnSteerCheck5(object sender, EventArgs e)
        {
            if (SteerCheck5.Checked)
            {
                int SteerValue = SteerSlide5.Value;
                string CMD_Steer = SteerCommand(5, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnSteerCheck6(object sender, EventArgs e)
        {
            if (SteerCheck6.Checked)
            {
                int SteerValue = SteerSlide6.Value;
                string CMD_Steer = SteerCommand(6, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

       
        private void OnSteerScroll3(object sender, MouseEventArgs e)
        {
            if (SteerCheck3.Checked)
            {
                int SteerValue = SteerSlide3.Value;
                string CMD_Steer = SteerCommand(3, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnSteerScroll4(object sender, MouseEventArgs e)
        {
            if (SteerCheck4.Checked)
            {
                int SteerValue = SteerSlide4.Value;
                string CMD_Steer = SteerCommand(4, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnSteerScroll5(object sender, MouseEventArgs e)
        {
            if (SteerCheck5.Checked)
            {
                int SteerValue = SteerSlide5.Value;
                string CMD_Steer = SteerCommand(5, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnSteerScroll6(object sender, MouseEventArgs e)
        {
            if (SteerCheck6.Checked)
            {
                int SteerValue = SteerSlide6.Value;
                string CMD_Steer = SteerCommand(6, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void OnLeftUPClick(object sender, MouseEventArgs e)
        {
            SendData(CMD_LeftForward);
        }

        private void OnUPClick(object sender, MouseEventArgs e)
        {
            //SendData(CMD_Forward);
            Thread t;
            t = new Thread(delegate()
            {
                SendData(CMD_Forward);
            });
            t.Start();
        }

        private void OnLeftClick(object sender, MouseEventArgs e)
        {
            SendData(CMD_TurnLeft);
        }

        private void OnRightClick(object sender, MouseEventArgs e)
        {
            SendData(CMD_TurnRight);
        }

        private void OnLeftDownClick(object sender, MouseEventArgs e)
        {
            SendData(CMD_LeftBackward);
        }

        private void OnDownClick(object sender, MouseEventArgs e)
        {
            SendData(CMD_Backward);
        }

        private void OnRightDown(object sender, MouseEventArgs e)
        {
            SendData(CMD_RightBackward);
        }

        private void OnRightUPClick(object sender, MouseEventArgs e)
        {
            SendData(CMD_RightForward);
        }

     
        private void OnCustomClick(object sender, EventArgs e)
        {
            if (!CustomSwitch)
            {
                CustomSwitch = true;
                SendData(CMD_Custom);
            }
            else
            {
                CustomSwitch = false;
                SendData(CMD_CustomOff);
            }
            
        }

        private void SteerSlide1_Scroll(object sender, MouseEventArgs e)
        {
            if (SteerCheck1.Checked)
            {
                int SteerValue = SteerSlide1.Value;
                string CMD_Steer = SteerCommand(1, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

        private void SteerSlide2_Scroll(object sender, MouseEventArgs e)
        {
            if (SteerCheck2.Checked)
            {
                int SteerValue = SteerSlide2.Value;
                string CMD_Steer = SteerCommand(2, (byte)SteerValue);
                SendData(CMD_Steer);
            }
        }

       

     

       

       
  
      /*  private void ContexRecognition(int iIndex, object obj, SpeechLib.SpeechRecognitionType type, SpeechLib.ISpeechRecoResult result)
        {

            textBox1.Text = result.PhraseInfo.GetText(0, -1, true);


        }*/

       

    }
}
