using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Upgrade
{
    public partial class Form1 : Form
    {
        private void wedgitInit()
        {
            UpgradeType.SelectedIndex = 0;//默认升级类型为普通升级
            sLockType.SelectedIndex = 2;//默认选择sLock-3
            DateTime mValue = EndDatePacker.Value;
            Times mTimes = new Times();

            EndDatePacker.Value = mTimes.ToDateTime((mTimes.ToUnixTime(mValue) + 31536000L).ToString());
            SoftwareVersion.Text = "0x03";
            HardwareVersion.Text = "";
            MCUComboBox.SelectedIndex = 0;
            FingerComboBox.SelectedIndex = 1;
            PasswordComboBox.SelectedIndex = 1;
            WifiComboBox.SelectedIndex = 0;
            RFIDComboBox.SelectedIndex = 0;
            AlarmCheckBox.Checked = false;
        }

        public Form1()
        {
            InitializeComponent();
            wedgitInit();
        }

        private void HandInfoSet(String file)
        {
            Documents mDocuments = new Documents(file);
            Handler mHandler = mDocuments.getM2Handler();
            if (mHandler == null)
            {
                MessageBox.Show("M2文件的数据不正确");
                return;
            }
            Times mTimes = new Times();
            StartDatePicker.Value = mTimes.ToDateTime(mHandler.start_time.ToString());
            EndDatePacker.Value = mTimes.ToDateTime(mHandler.end_time.ToString());
            startSerial.Text = mHandler.MacToString(mHandler.start_sn, (UInt16)(mHandler.start_sn.Length));
            endSerial.Text = mHandler.MacToString(mHandler.end_sn, (UInt16)(mHandler.end_sn.Length));
            LockMAC.Text = mHandler.MacToString(mHandler.mac, (UInt16)(mHandler.mac.Length));
            sLockType.SelectedIndex = mHandler.GetLockType();
            UpgradeType.SelectedIndex = mHandler.type;

            SoftwareVersion.Text = "0x" + Convert.ToString(mHandler.sw_ver,16);
            HardwareVersion.Text = "0x" + Convert.ToString(mHandler.hw_ver,16);

        }

        private void OnLoadFile(object sender, EventArgs e)
        {
            String LoadFilePath = "";
            if (loadUpgradeFile.ShowDialog() == DialogResult.OK)
            {
                LoadFilePath = loadUpgradeFile.FileName;
                LoadFileText.Text = LoadFilePath;
                Documents mDocuments = new Documents(LoadFilePath);
                if (mDocuments.getType() == Documents.FileType._FILE_HEX)
                {
                    FileInfo minfo = mDocuments.getFileInfo();

                    FileInfoText.Text = "Hex 文件\n" + "文件名:" + minfo.Name + "\n文件大小:" + minfo.Length +"Byte" +"("+(UInt32)(minfo.Length / 1024.0) + "KB)\n";
                }
                else if (mDocuments.getType() == Documents.FileType._FILE_M2)
                {
                    FileInfo minfo = mDocuments.getFileInfo();
                    FileInfoText.Text = "M2 文件\n" + "文件名:" + minfo.Name + "\n文件大小:" + minfo.Length + "Byte" + "(" + (UInt32)(minfo.Length / 1024.0) + "KB)\n";
                    HandInfoSet(LoadFilePath);
                }
                else if (mDocuments.getType() == Documents.FileType._FILE_BIN)
                {
                    FileInfo minfo = mDocuments.getFileInfo();
                    FileInfoText.Text = "Bin 文件\n" + "文件名:" + minfo.Name + "\n文件大小:" + minfo.Length + "Byte" + "(" + (UInt32)(minfo.Length / 1024.0) + "KB)\n";
                }
                else
                {
                    FileInfoText.Text = "不是有效的文件格式，请重新选择文件";
                }
            } 
        }

        private void OnPackFile(object sender, EventArgs e)
        {
            if (saveUpgradeFile.ShowDialog() == DialogResult.OK)
            {
               SaveFileText.Text = saveUpgradeFile.FileName;
            }
            /*
            Encrypt mEncrypt = new Encrypt();
            String encryptStr = null;
            byte[] enbyte = null;
            byte[] xorByte = new byte[20];//{ 0x43, 0xF4, 0x8D, 0xF1, 0x91, 0xB8, 0x6F, 0xA0, 0x18, 0x89, 0x10, 0x70, 0x20, 0x7A, 0xA6, 0xF6 };
            for (int i = 0; i < xorByte.Length; i++)
            {
                xorByte[i] = (byte)0xee;
            }

            encryptStr = mEncrypt.AESEncrypt(xorByte);

            MessageBox.Show("Enctypt  Str:" + encryptStr + "\n len:" + encryptStr.Length);*/
             
            //String mDecrypt =  mEncrypt.AESDecrypt(encryptStr);
            //byte[] debyte = Encoding.UTF8.GetBytes(mDecrypt);
             
            
        }

        private Boolean TimeCheck()
        {
            Times mTimes = new Times();
            DateTime startTime = StartDatePicker.Value.Date;
            DateTime endTime = EndDatePacker.Value.Date;
            if (DateTime.Compare(startTime, endTime) == 0)
            {
                MessageBox.Show("开始时间等于结束时间，请重新选择时间");
                return false;
            }
            else if (DateTime.Compare(startTime, endTime) > 0)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择时间");
                return false;
            }
            if (mTimes.Compare(startTime, endTime) == Times.TimeStatus._TIME_UNDER)
            {
                if (MessageBox.Show("输入的时间还未到，是否继续?", "", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return false;
                }
            }
            else if (mTimes.Compare(startTime, endTime) == Times.TimeStatus._TIME_OVERFLOW)
            {
                MessageBox.Show("输入的时间已经过期，请重新选择时间");
                return false;
            }
            return true;
        }

        private Boolean CheckMac(String mac)
        {
            Regex reg = new Regex(@"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$");
            Match m = reg.Match(mac);
            if (m.Success)
            {
                
                return true;
            }
            return false;
        }

        private Boolean CheckVersion(String ver)
        {
            Regex reg = new Regex(@"0x[0-9a-fA-F]+");
            Match m = reg.Match(ver);
            if (m.Success)
            {

                return true;
            }
            return false;
        }

        private Boolean CheckFileNull()
        {
            if ((LoadFileText.Text == "") || (SaveFileText.Text == ""))
            {
                MessageBox.Show("输入/输出文件为空，请选择具体文件");
                return false;
            }
            if (!File.Exists(LoadFileText.Text))
            {
                MessageBox.Show("输入文件为不存在，请选择具体文件");
                return false;
            }
            return true;
        }


        private byte getLockType()
        {
            byte LockType = 0;
            switch(sLockType.SelectedIndex)
            {
                case 0:
                    LockType = 4;
                    break;
                case 1:
                    LockType = 5;
                    break;
                case 2:
                    LockType = 3;
                    break;
                case 3:
                    LockType = 6;
                    break;
                case 4:
                    LockType = 8;
                    break;
                case 5:
                    LockType = 10;
                    break;
                case 6:
                    LockType = 9;
                    break;
                default:
                    break;
            }
            return LockType;
        }

        private void OnExecute(object sender, EventArgs e)
        {
            Times mTimes = new Times();
            Handler mHandler = new Handler();
            if (!TimeCheck())
            {
                return;
            }
            if (!CheckMac(startSerial.Text))
            {
                MessageBox.Show("开始序列号格式输入不正确，请重新输入");
                return;
            }
            mHandler.setStartSN(startSerial.Text);
            if (!CheckMac(endSerial.Text))
            {
                MessageBox.Show("结束序列号格式输入不正确，请重新输入");
                return;
            }
            mHandler.setEndSN(endSerial.Text);
            if (!CheckMac(LockMAC.Text))
            {
                MessageBox.Show("MAC格式输入不正确，请重新输入");
                return;
            }
            mHandler.setMAC(LockMAC.Text);
            mHandler.type = (byte)UpgradeType.SelectedIndex;
            mHandler.lockType = getLockType();
            if (!CheckVersion(SoftwareVersion.Text))
            {
                MessageBox.Show("软件版本号输入不正确，请重新输入");
                return;
            }
            mHandler.sw_ver = Convert.ToByte(SoftwareVersion.Text, 16);

            if (!CheckVersion(HardwareVersion.Text))
            {
                MessageBox.Show("硬件版本号输入不正确，请重新输入");
                return;
            }
            if (!CheckFileNull())
            {
                return;
            }
            Documents mDocuments = new Documents(LoadFileText.Text, SaveFileText.Text);

            if (mDocuments.getType() != Documents.FileType._FILE_M2)
            {
                FileInfo mFileinfo = mDocuments.getFileInfo();
                mHandler.hex_size = (UInt32)(mFileinfo.Length);
            }
            else 
            {
                Handler tmpHand = mDocuments.getM2Handler();
                mHandler.hex_size = tmpHand.hex_size;
            }
            
            mHandler.hw_ver = (UInt32)Convert.ToInt32(HardwareVersion.Text, 16);
            mHandler.start_time = mTimes.ToUnixTime(StartDatePicker.Value.Date);
            mHandler.end_time = mTimes.ToUnixTime(EndDatePacker.Value.Date);
            mHandler.crc = mHandler.getCrcValue();

          
            byte[] mHandByte = mHandler.getHandlerGroup();
            if (mDocuments.HexfilePacked(mHandByte, (UInt16)(mHandByte.Length)))
            {
                PacketData.Text = "长度:" + mHandler.lenght() +" 字节\n" + mHandler.ByteToString(mHandByte, (UInt32)(mHandByte.Length));
                MessageBox.Show("文件写入成功");
            }
            else
            {
                MessageBox.Show("文件写入失败");
            }
        }



        private void OnHardwareClick(object sender, EventArgs e)
        {
            int HardwareVer = 0;
            int AlarmEnable = 0;
            if(AlarmCheckBox.Checked)
            {
                AlarmEnable = 1;
            }
            else
            {
                 AlarmEnable = 0;  
            }

            HardwareVer = MCUComboBox.SelectedIndex << 30 | FingerComboBox.SelectedIndex << 27 | PasswordComboBox.SelectedIndex<<25|
                           WifiComboBox.SelectedIndex << 23 | RFIDComboBox.SelectedIndex << 21 | AlarmEnable<<20;
           // String.Format("{0:X8}", HardwareVer)
            HardwareVersion.Text = "0x"+String.Format("{0:X8}", HardwareVer);//Convert.ToString(1, 16);
        }
    }
}
