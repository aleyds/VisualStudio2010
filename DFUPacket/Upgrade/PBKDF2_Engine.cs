using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Upgrade
{
    class PBKDF2_Engine
    {
        private readonly IMac hMac = new HMac(new Sha1Digest());
        private  string salt = "4XIV9xUtD7WvV5Qf";
        private char[] hexArray = "0123456789ABCDEF".ToCharArray();


        private void F(
            byte[] P,
            byte[] S,
            int c,
            byte[] iBuf,
            byte[] outBytes,
            int outOff)
        {
            byte[] state = new byte[hMac.GetMacSize()];
            ICipherParameters param = new KeyParameter(P);

            hMac.Init(param);

            if (S != null)
            {
                hMac.BlockUpdate(S, 0, S.Length);
            }

            hMac.BlockUpdate(iBuf, 0, iBuf.Length);

            hMac.DoFinal(state, 0);

            Array.Copy(state, 0, outBytes, outOff, state.Length);

            for (int count = 1; count != c; count++)
            {
                hMac.Init(param);
                hMac.BlockUpdate(state, 0, state.Length);
                hMac.DoFinal(state, 0);

                for (int j = 0; j != state.Length; j++)
                {
                    outBytes[outOff + j] ^= state[j];
                }
            }
        }

        private void IntToOctet(
            byte[] Buffer,
            int i)
        {
            Buffer[0] = (byte)((uint)i >> 24);
            Buffer[1] = (byte)((uint)i >> 16);
            Buffer[2] = (byte)((uint)i >> 8);
            Buffer[3] = (byte)i;
        }

        public byte[] PBKDF2Gen(string password, int iterations, int keylen, int bit)
        {
            string ret;
            byte[] pwd = ASCIIEncoding.UTF8.GetBytes(password);
            int dklen = keylen * bit;
            int mIterationCount = iterations;
            byte[] output = GenerateDerivedKey(keylen, pwd, mIterationCount);
            ret = bytesToHex(output);
            return output;
        }

       
	    public  String bytesToHex(byte[] bytes) {
	    char[] hexChars = new char[bytes.Length * 2];
        for (int j = 0; j < bytes.Length; j++)
        {
	        int v = bytes[j] & 0xFF;
	        hexChars[j * 2] = hexArray[v >> 4];
	        hexChars[j * 2 + 1] = hexArray[v & 0x0F];
	    }
        char[] slash_hex_chars = new char[(hexChars.Length * 3) / 2];
	    int jj = 0;
        for (int i = 0; i < slash_hex_chars.Length; i++)
        {
	    	if(i % 3 == 0) {
	    		slash_hex_chars[i] = '\\';
	    	}
	    	else {
	    		slash_hex_chars[i] = hexChars[jj];
	    		jj++;
	    	}
	    }
	    return new String(slash_hex_chars);
	}

        // Use this function to retrieve a derived key.
        // dkLen is in octets, how much bytes you want when the function to return.
        // mPassword is the password converted to bytes.
        // mSalt is the salt converted to bytes
        // mIterationCount is the how much iterations you want to perform. 


        public byte[] GenerateDerivedKey(
            int dkLen,
            byte[] mPassword,
            int mIterationCount
            )
        {
            int hLen = hMac.GetMacSize();
            int l = (dkLen + hLen - 1) / hLen;
            byte[] iBuf = new byte[4];
            byte[] outBytes = new byte[l * hLen];
            byte[] msalt = ASCIIEncoding.UTF8.GetBytes(salt);

            for (int i = 1; i <= l; i++)
            {
                IntToOctet(iBuf, i);

                F(mPassword, msalt, mIterationCount, iBuf, outBytes, (i - 1) * hLen);
            }

            //By this time outBytes will contain the derived key + more bytes.
            // According to the PKCS #5 v2.0: Password-Based Cryptography Standard (www.truecrypt.org/docs/pkcs5v2-0.pdf) 
            // we have to "extract the first dkLen octets to produce a derived key".

            //I am creating a byte array with the size of dkLen and then using
            //Buffer.BlockCopy to copy ONLY the dkLen amount of bytes to it
            // And finally returning it :D

            byte[] output = new byte[dkLen];

            Buffer.BlockCopy(outBytes, 0, output, 0, dkLen);

            return output;
        }
    }
}
