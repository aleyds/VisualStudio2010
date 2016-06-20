using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upgrade
{
    class Handler
    {
        private const UInt16 SYNC_DATE = 0xE41B;
        private const UInt16 DATA_LEN = 40;
        private const byte PRODUCER_DATA = 0x29;

        private UInt16 sync;
        private UInt16 len;
        private byte producer;
        public UInt32 start_time;
        public UInt32 end_time;
        public byte[] start_sn = new byte[6];
        public byte[] end_sn = new byte[6];
        public byte[] mac = new byte[6];
        public byte type;
        public byte lockType;
        public byte sw_ver;
        public UInt32 hw_ver;
        public UInt32 hex_size;
        public UInt16 crc;

        public Handler()
        {
            sync = SYNC_DATE;
            len = DATA_LEN;
            producer = PRODUCER_DATA;
        }

        public UInt16 lenght()
        {
            return (UInt16)(len + 4);
        }


        public byte GetLockType()
        {
            byte selectType = 2; 
            switch (lockType)
            {
                case 4:
                    selectType = 0;
                    break;
                case 5:
                    selectType = 1;
                    break;
                case 3:
                    selectType = 2;
                    break;
                case 6:
                    selectType = 3;
                    break;
                case 8:
                    selectType = 4;
                    break;
                case 10:
                    selectType = 5;
                    break;
                case 9:
                    selectType = 6;
                    break;
                default:
                    break;
            }
            return selectType;
        }

        public Boolean setHandlerData(byte[] data)
        {
            sync = (UInt16)((data[0] << 8) | data[1]);
            if (sync != SYNC_DATE)
            {
                return false;
            }
            len = (UInt16)((data[2] << 8) | data[3]);
            producer = data[4];
            start_time = (UInt32)((data[5] << 24) | (data[6] << 16) | (data[7] << 8) | (data[8]));
            end_time = (UInt32)((data[9] << 24) | (data[10] << 16) | (data[11] << 8) | (data[12]));
            for (int i = 0; i < 6; i++)
            {
                start_sn[i] = data[13 + i];
            }
            for (int i = 0; i < 6; i++)
            {
                end_sn[i] = data[19 + i];
            }
            for (int i = 0; i < 6; i++)
            {
                mac[i] = data[25 + i];
            }
            lockType = data[31];
            type = data[32];
            sw_ver = data[33];
            hw_ver = (UInt32)((data[34] << 24) | (data[35] << 16) | (data[36] << 8) | (data[37]));
            hex_size = (UInt32)((data[38] << 24) | (data[39] << 16) | (data[40] << 8) | (data[41]));
            UInt16 dataCrc = (UInt16)crc16(data, (UInt16)(lenght() - 2), 0);
            
            crc = (UInt16)((data[42] << 8) | data[43]);
            if (dataCrc != crc)
            {
                return false;
            }
            return true;
        }

        public UInt16 crc16(byte[] pdata, UInt16 len, UInt16 exCrc)
        {
            UInt16 mcrc = (UInt16)((exCrc == 0) ? 0xffff : exCrc);
            for (int i = 0; i < len; i++)
            {
                mcrc = (UInt16)((mcrc >> 8) | (mcrc << 8));
                mcrc ^= (pdata[i]);
                mcrc ^= (UInt16)((mcrc & 0xff) >> 4);
                mcrc ^= (UInt16)((mcrc << 8) << 4);
                mcrc ^= (UInt16)(((mcrc & 0xff) << 4) << 1); 
            }
            return mcrc;
        }

        public UInt16 getCrcValue()
        {
            byte[] handByte = getHandlerGroup();
            return crc16(handByte, (UInt16)(lenght() - 2), 0);
        }

        public String ByteToString(byte[] ByteData, UInt32 len)
        {
            String outString = "";
            
            for (int i = 0; i < len; i++)
            {
                outString += "0x" + Convert.ToString(ByteData[i], 16) + " ";
            }
            return outString;
        }

        public byte[] getHandlerGroup()
        {
            byte[] mHandlerData = new byte[(int)(len + 4)];
            mHandlerData[0] = (byte)((sync >> 8)&0xff);
            mHandlerData[1] = (byte)((sync) & 0xff);
            mHandlerData[2] = (byte)((len >> 8) & 0xff);
            mHandlerData[3] = (byte)((len) & 0xff);
            mHandlerData[4] = (byte)((producer) & 0xff);
            mHandlerData[5] = (byte)((start_time >> 24) & 0xff);
            mHandlerData[6] = (byte)((start_time >> 16) & 0xff);
            mHandlerData[7] = (byte)((start_time >> 8) & 0xff);
            mHandlerData[8] = (byte)((start_time) & 0xff);
            mHandlerData[9] = (byte)((end_time >> 24) & 0xff);
            mHandlerData[10] = (byte)((end_time >> 16) & 0xff);
            mHandlerData[11] = (byte)((end_time >> 8) & 0xff);
            mHandlerData[12] = (byte)((end_time) & 0xff);
            for (int i = 0; i < 6; i++)
            {
                mHandlerData[13 + i] = start_sn[i];
            }
            for (int i = 0; i < 6; i++)
            {
                mHandlerData[19 + i] = end_sn[i];
            }
            for (int i = 0; i < 6; i++)
            {
                mHandlerData[25 + i] = mac[i];
            }
            mHandlerData[31] = lockType;
            mHandlerData[32] = type;
            mHandlerData[33] = sw_ver;
            mHandlerData[34] = (byte)((hw_ver >> 24) & 0xff);
            mHandlerData[35] = (byte)((hw_ver >> 16) & 0xff);
            mHandlerData[36] = (byte)((hw_ver >> 8) & 0xff);
            mHandlerData[37] = (byte)((hw_ver) & 0xff);
            mHandlerData[38] = (byte)((hex_size >> 24) & 0xff);
            mHandlerData[39] = (byte)((hex_size >> 16) & 0xff);
            mHandlerData[40] = (byte)((hex_size >> 8) & 0xff);
            mHandlerData[41] = (byte)((hex_size) & 0xff);
            mHandlerData[42] = (byte)((crc >> 8) & 0xff);
            mHandlerData[43] = (byte)((crc) & 0xff);
            return mHandlerData;
        }

        public Handler(UInt32 stime, UInt32 etime, byte ptype, byte psw_ver, UInt32 phw_ver)
        {
            sync = SYNC_DATE;
            len = DATA_LEN;
            producer = PRODUCER_DATA;
            start_time = stime;
            type = ptype;
            sw_ver = psw_ver;
            hw_ver = phw_ver;
        }

        private void ToMac(String macStr, byte[] macArray)
        {
            string tmpMac = macStr.Replace(":","");
            for (int i = 0; i < macArray.Length; i++)
            {

                macArray[i] = Convert.ToByte(tmpMac.Substring(i * 2, 2), 16);
            }
        }

        public String MacToString(byte[] mac, UInt16 len)
        {
           return  BitConverter.ToString(mac).Replace("-",":");
        }

        public void setStartSN(String sn)
        {
            ToMac(sn, start_sn);
        }

        public void setEndSN(String sn)
        {
            ToMac(sn, end_sn);
        }

        public void setMAC(String sn)
        {
            ToMac(sn, mac);
        }
        
    }
}
