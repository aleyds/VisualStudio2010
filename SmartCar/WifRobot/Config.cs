using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WifiVideo
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }
        public string FileName; //INI文件名
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        private void Config_Load(object sender, EventArgs e)
        {
            GetIni();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteIni("VideoUrl","videourl",this.textBoxVideo.Text);
            WriteIni("VideoUrl", "PicturePath", this.savePictureText.Text);
            WriteIni("ControlUrl", "controlUrl", this.textControlURL.Text);
            WriteIni("ControlPort", "controlPort", this.textBoxControlPort.Text);
            WriteIni("ControlCommand", "CMD_Forward", this.txtForward.Text);
            WriteIni("ControlCommand", "CMD_Backward", this.txtBackward.Text);
            WriteIni("ControlCommand", "CMD_TurnLeft", this.txtLeft.Text);
            WriteIni("ControlCommand", "CMD_TurnRight", this.txtRight.Text);
            WriteIni("ControlCommand", "CMD_Stop", this.txtStop.Text);
            WriteIni("ControlCommand", "CMD_LeftForward", this.textLeftUP.Text);
            WriteIni("ControlCommand", "CMD_RightForward", this.textRightUP.Text);
            WriteIni("ControlCommand", "CMD_LeftBackward", this.textLeftDown.Text);
            WriteIni("ControlCommand", "CMD_RightBackward", this.textRightDown.Text);
            WriteIni("ControlCommand", "CMD_TorchOn", this.textTorchOn.Text);
            WriteIni("ControlCommand", "CMD_TorchOff", this.textTorchOff.Text);
            WriteIni("ControlCommand", "CMD_AlarmOn", this.textAlarmOn.Text);
            WriteIni("ControlCommand", "CMD_AlarmOff", this.textAlarmOff.Text);
            WriteIni("ControlCommand", "CMD_Custom", this.textcustom.Text);
             
            //WriteIni("ControlCommand", "CMD_EngineLeft", this.txtEngineLeft.Text);
            //WriteIni("ControlCommand", "CMD_EngineUpRest", this.txtEngineUpRest.Text);
         /*  WriteIni("ControlCommand", "CMD_EngineRight", this.txtEngineRight.Text);
            WriteIni("ControlCommand", "CMD_EngineUp", this.txtEngineUp.Text);
            WriteIni("ControlCommand", "CMD_EngineDownRest", this.txtEngineDownRest.Text);
            WriteIni("ControlCommand", "CMD_EngineDown", this.txtEngineDown.Text);*/


            MessageBox.Show("配置成功！请重启程序以使配置生效!", "配置信息", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            this.Close();
        }
        //写INI文件
        public void WriteIni(string Section, string Ident, string Value)
        {
            if (!WritePrivateProfileString(Section, Ident, Value, FileName))
            {

                throw (new ApplicationException("写入配置文件出错"));
            }
          
        }
        //读取INI文件指定
        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
        private void GetIni()
        {    //Port
            FileName = Application.StartupPath + "\\Config.ini";
            this.textBoxVideo.Text = ReadIni("VideoUrl", "videourl", "");
            this.savePictureText.Text = ReadIni("VideoUrl", "PicturePath", "");
            
            this.textControlURL.Text = ReadIni("ControlUrl", "controlUrl", "");
            this.textBoxControlPort.Text = ReadIni("ControlPort", "controlPort", "");
            //moto
            this.txtForward.Text = ReadIni("ControlCommand", "CMD_Forward", "");
            this.txtBackward.Text = ReadIni("ControlCommand", "CMD_Backward", "");
            this.txtLeft.Text = ReadIni("ControlCommand", "CMD_TurnLeft", "");
            this.txtRight.Text = ReadIni("ControlCommand", "CMD_TurnRight", "");
            this.txtStop.Text = ReadIni("ControlCommand", "CMD_Stop", "");

            this.textLeftUP.Text = ReadIni("ControlCommand", "CMD_LeftForward", "");
            this.textRightUP.Text = ReadIni("ControlCommand", "CMD_RightForward", "");
            this.textLeftDown.Text = ReadIni("ControlCommand", "CMD_LeftBackward", "");
            this.textRightDown.Text = ReadIni("ControlCommand", "CMD_RightBackward", "");
            this.textTorchOn.Text = ReadIni("ControlCommand", "CMD_TorchOn", "");
            this.textTorchOff.Text = ReadIni("ControlCommand", "CMD_TorchOff", "");
            this.textAlarmOn.Text = ReadIni("ControlCommand", "CMD_AlarmOn", "");
            this.textAlarmOff.Text = ReadIni("ControlCommand", "CMD_AlarmOff", "");
            this.textcustom.Text = ReadIni("ControlCommand", "CMD_Custom", "");

         
            //Engine
           // this.txtEngineLeft.Text = ReadIni("ControlCommand", "CMD_EngineLeft", "");
            //this.txtEngineUpRest.Text = ReadIni("ControlCommand", "CMD_EngineUpRest", "");
          /*  this.txtEngineRight.Text = ReadIni("ControlCommand", "CMD_EngineRight", "");

            this.txtEngineUp.Text = ReadIni("ControlCommand", "CMD_EngineUp", "");
            this.txtEngineDownRest.Text = ReadIni("ControlCommand", "CMD_EngineDownRest", "");
            this.txtEngineDown.Text = ReadIni("ControlCommand", "CMD_EngineUpDown", "");*/
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEngineUp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEngineDown_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEngineRight_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEngineUp_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEngineDownRest_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEngineDown_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtForward_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLeft_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxVideo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textControlURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxControlPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void OnPictureSaveClick(object sender, EventArgs e)
        {
            if (folderBrowserPicture.ShowDialog() == DialogResult.OK)
            {
                string Path = folderBrowserPicture.SelectedPath;
                savePictureText.Text = Path;
            }
        }

    }
}
