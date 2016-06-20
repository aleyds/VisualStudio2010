using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Upgrade
{
    class Documents
    {
        private const String HEX_FILE = "hex";
        private const String M2_FILE = "M2";
        private const String BIN_FILE = "bin";

        private const int BUFFER_LEN = 20;
        private const int HAND_LEN = 43;

        public enum FileType { 
            _FILE_HEX = 0,
            _FILE_M2 = 1,
            _FILE_BIN = 2,
            _FILE_NO,
        };

        private String mDocumentPath;
        private String mOutputPath;

        public Documents(String Path)
        {
            mDocumentPath = Path;
        }

        public Documents(String input, String output)
        {
            mDocumentPath = input;
            mOutputPath = output;
        }

        public FileType getType()
        {
            string suffix = mDocumentPath.Substring(mDocumentPath.LastIndexOf('.') + 1);

            if (String.Compare(HEX_FILE, suffix, true) == 0)
            {

                return FileType._FILE_HEX;
            }
            else if (String.Compare(M2_FILE, suffix, true) == 0)
            {
                return FileType._FILE_M2;
            }
            else if (String.Compare(BIN_FILE, suffix, true) == 0)
            {
                return FileType._FILE_BIN;
            }
            else
            {
                return FileType._FILE_NO;
            }
        }

        

        public FileInfo getFileInfo()
        {
            if (File.Exists(mDocumentPath))
            {
                return new FileInfo(mDocumentPath);
            }
            return null;
        }

        private Boolean FilePackedHex(byte[] hand, UInt16 len)
        {
            int readByte = 0;
            byte[] ReadBuffer = new byte[BUFFER_LEN];
            FileStream outfp = new FileStream(mOutputPath, FileMode.Create);
            outfp.Write(hand, 0, len);
            FileStream infp = new FileStream(mDocumentPath, FileMode.Open);
            Encrypt mEncrypt = new Encrypt();
            byte[] enbyte = null;
            String encryptStr = null;
            byte[] xorByte = null;
            int mLenght = 0;
            while ((readByte = infp.Read(ReadBuffer, 0, BUFFER_LEN)) > 0)
            {
                xorByte = mEncrypt.M2Encrypt(ReadBuffer, (UInt32)(ReadBuffer.Length));
                encryptStr = mEncrypt.AESEncrypt(xorByte);
                mLenght = encryptStr.Length;
                enbyte = Encoding.UTF8.GetBytes(encryptStr);
                //byte[] enbyte = Encoding.UTF8.GetBytes(encryptStr);
                outfp.Write(enbyte, 0, enbyte.Length);
            }

            outfp.Close();
            infp.Close();
            return true;
        }



        private Boolean FilePackedBin(byte[] hand, UInt16 len)
        {
            int readByte = 0;
            byte[] ReadBuffer = new byte[BUFFER_LEN];
            FileStream outfp = new FileStream(mOutputPath, FileMode.Create);
            outfp.Write(hand, 0, len);
            FileStream infp = new FileStream(mDocumentPath, FileMode.Open);
            Encrypt mEncrypt = new Encrypt();
            byte[] enbyte = null;
            String encryptStr = null;
            byte[] xorByte = new byte[BUFFER_LEN];
            int mLenght = 0;
            byte[] binAes = new byte[BUFFER_LEN-4];
            byte[] bindecrypt = null;
            while ((readByte = infp.Read(ReadBuffer, 0, BUFFER_LEN)) > 0)
            {
                Array.Copy(ReadBuffer, 0, binAes, 0, (BUFFER_LEN - 4));
                bindecrypt = mEncrypt.BinDecrypt(binAes);
                Array.Copy(bindecrypt, 0, xorByte, 0, (BUFFER_LEN - 4));
                Array.Copy(ReadBuffer, (BUFFER_LEN - 4), xorByte, (BUFFER_LEN - 4), (4));
                encryptStr = mEncrypt.AESEncrypt(xorByte);
                mLenght = encryptStr.Length;
                enbyte = Encoding.UTF8.GetBytes(encryptStr);
                //byte[] enbyte = Encoding.UTF8.GetBytes(encryptStr);
                outfp.Write(enbyte, 0, enbyte.Length);
            }

            outfp.Close();
            infp.Close();
            return true;
        }

        private Boolean FileReplaceHand(byte[] hand, UInt16 len)
        {
            int readByte = 0;
            byte[] ReadBuffer = new byte[BUFFER_LEN];
            FileStream outfp = new FileStream(mOutputPath, FileMode.Create);
            outfp.Write(hand, 0, len);
            FileStream infp = new FileStream(mDocumentPath, FileMode.Open);
            //infp.Read(ReadBuffer, 0, 43);
            infp.Position = HAND_LEN;

            while ((readByte = infp.Read(ReadBuffer, 0, BUFFER_LEN)) > 0)
            {
                outfp.Write(ReadBuffer, 0, readByte);
            }

            outfp.Close();
            infp.Close();
            return true;
        }

        public Boolean HexfilePacked(byte[] hand, UInt16 len)
        {
            if (getType() == FileType._FILE_HEX)
            {
                return FilePackedHex(hand, len);
            }
            else if (getType() == FileType._FILE_BIN)
            {
                return FilePackedBin(hand, len);
            }
            else if (getType() == FileType._FILE_M2)
            {
                return FileReplaceHand(hand, len);
            }
            return false;
            
        }

        public Handler getM2Handler()
        {
            if (getType() != FileType._FILE_M2)
            {
                return null;
            }
            Handler mHandler = new Handler();
            FileStream fp = new FileStream(mDocumentPath, FileMode.Open);
            byte[] buffer = new byte[(int)mHandler.lenght()];
            fp.Read(buffer, 0, buffer.Length);
            fp.Close();

            if (mHandler.setHandlerData(buffer))
            {
                return mHandler;
            }
            return null;
        }
    }
}
