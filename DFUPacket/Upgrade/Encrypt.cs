using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;



namespace Upgrade
{
    class Encrypt
    {
        private String Passwoord = "4XIV9xUtD7WvV5DA";
        private byte[] Key_IV = null;
        private byte[] Key = null;
        private byte[] LockKey = null;

        public Encrypt()
        {
            PBKDF2_Engine PBKD = new PBKDF2_Engine();
            Key = PBKD.PBKDF2Gen(Passwoord, 1000, 16, 8);
            Key_IV = PBKD.PBKDF2Gen(Passwoord, 500, 16, 8);
            LockKey = PBKD.PBKDF2Gen(Passwoord, 200, 16, 8);
        }

        private byte xorByte(byte b)
        {
            byte ret_code = (byte)(b ^ LockKey[0]);
            for (int i = 0; i < LockKey.Length; i++)
            {
                ret_code ^= LockKey[i];
            }
            return ret_code;
        }

        public byte[] M2Encrypt(byte[] buffer, UInt32 len)
        {
            byte[] retByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                retByte[i] = xorByte(buffer[i]);
            }
            return retByte;
        }

        private byte[] xorECB(byte[] data)
        {
            int i = 0;
            byte[] output = new byte[data.Length];
            for (i = 0; i < data.Length; i++)
            {
                output[i] = (byte)(data[i] ^ Key_IV[i]);
            }
            return output;
        }

        public byte[] BinEncrypt(byte[] input)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = xorECB(input);


            des.Key = LockKey;//Encoding.UTF8.GetBytes(keyStr);
            //des.IV = Key_IV;//Encoding.UTF8.GetBytes(Key_IV);
            des.Mode = CipherMode.ECB;
            des.BlockSize = 128;
            des.FeedbackSize = 16;
            des.Padding = PaddingMode.None;
   

            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    //cs.Flush();
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();//得到加密后的字节数组

                    cs.Close();
                    ms.Close();
                }
            }
            return cipherBytes;
        }

        

        public byte[] BinDecrypt(byte[] input)
        {
            byte[] cipherText = input;//xorECB(input);

            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = LockKey;//Encoding.UTF8.GetBytes(Key);
            des.Mode = CipherMode.ECB;
            des.BlockSize = 128;
            des.FeedbackSize = 16;
            des.Padding = PaddingMode.None;
            //des.IV = Key_IV;// Encoding.UTF8.GetBytes(Key_IV);
            byte[] decryptBytes = new byte[cipherText.Length];

            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return xorECB(decryptBytes);
        }

        public String AESEncrypt(byte[] plainText)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = plainText;//Encoding.UTF8.GetBytes(plainText);


            des.Key = Key;//Encoding.UTF8.GetBytes(keyStr);
            des.IV = Key_IV;//Encoding.UTF8.GetBytes(Key_IV);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
      
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();//得到加密后的字节数组
                    cs.Close();
                    ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }

        public String AESDecrypt(string showText)
        {
            byte[] cipherText = Convert.FromBase64String(showText);

            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Key;//Encoding.UTF8.GetBytes(Key);
            des.IV = Key_IV;// Encoding.UTF8.GetBytes(Key_IV);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return Encoding.UTF8.GetString(decryptBytes);

        }
    }
}
