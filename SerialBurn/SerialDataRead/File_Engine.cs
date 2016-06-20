using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace SerialDataRead
{
    class File_Engine
    {
        private char[] hexArray = "0123456789ABCDEF".ToCharArray();
        private  const int CmdSendTime = 1000;

        public void SavePasswordFile(string SrcFile, string dstFile,string code)
        {
            StreamReader reader = new StreamReader(SrcFile);
            StreamWriter write = new StreamWriter(dstFile);
            string readLine = "";
            string writeLine = "";
            while ((readLine = reader.ReadLine()) != null)
            {

                if (readLine.Contains("PWDROOM$ = "))
                {
                    writeLine = "PWDROOM$ = " + code;
                }
                else 
                {
                    writeLine = readLine;
                }
                write.WriteLine(writeLine);
                write.Flush();
            }
            reader.Close();
            write.Close();

        }//end SavePasswordFile


        public void XCompileFile(string xcomName,String File)
        {
            Process p;
            ProcessStartInfo Proinfo = new ProcessStartInfo();
            Proinfo.FileName = xcomName;
            Proinfo.Arguments = File;
            p = Process.Start(Proinfo);
            p.WaitForExit();

        }//end XCompileFile

        public string bytesToHexExt(byte[] bytes, int len)
        {
            char[] hexChars = new char[len * 2];
            for (int n = 0; n < len; n++)
            {
                int v = bytes[n] & 0xFF;
                hexChars[n * 2] = hexArray[v >> 4];
                hexChars[n * 2 + 1] = hexArray[v & 0x0F];
            }
            char[] slash_hex_chars = new char[hexChars.Length];
            int j = 0;
            for (int i = 0; i < slash_hex_chars.Length; i++)
            {

                slash_hex_chars[i] = hexChars[j];
                j++;

            }
            return new string(slash_hex_chars);
        }

        public void SendSerialData(string Data, SerialPort port, WriteData FormData)
        {
            //Console.WriteLine(Data);
           
            FormData.SetMessage(Data+"\r\n");
            Data += "\r\n\r\n";

            byte[] WriteBuffer = Encoding.ASCII.GetBytes(Data);
            port.Write(WriteBuffer, 0, WriteBuffer.Length);
            
        
        }

        public string ReadSerialData(SerialPort port)
        {
            string back = port.ReadExisting();
            if (back.Equals(""))
            {
                // SerialPortClose();
                return back;
            }
            //FeedBackData += back;
            Console.WriteLine(back);
            return back;
        }

        public void smartBASICWriteAutorun(string Path, SerialPort port, WriteData FormData)
        {

            string ATCmd = "";
            int firstIndex = Path.LastIndexOf("\\");
            int endIndex = Path.LastIndexOf(".");
            string pgName = Path.Substring(firstIndex + 1, endIndex - firstIndex - 1);

            
            ATCmd = "ATZ";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT&F 1";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT I 0";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);
            //Console.WriteLine(ATCmd);

            ATCmd = "AT I 13";
            SendSerialData(ATCmd, port, FormData);
            
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT+DEL " + "\"" + pgName + "\"" + "+";
            //Console.WriteLine(ATCmd);
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT+FOW " + "\"" + pgName + "\"";
            //Console.WriteLine(ATCmd);
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            SendFileData(Path, port, FormData);

            ATCmd = "AT+FCL";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT+RUN " + "\"" + pgName + "\"";
            SendSerialData(ATCmd, port, FormData);

            Console.WriteLine("+++++DONE AUTORUN++++++");
            
             



        }

        
        public void smartBASICWrite(string Path, SerialPort port,WriteData FormData)
        {

            string ATCmd = "";
            int firstIndex = Path.LastIndexOf("\\");
            int endIndex = Path.LastIndexOf(".");
            string pgName = Path.Substring(firstIndex + 1, endIndex - firstIndex - 1);

            ATCmd = "ATZ";
            SendSerialData(ATCmd, port, FormData);
         
            
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT&F *";
            SendSerialData(ATCmd, port, FormData);
            
            Thread.Sleep(CmdSendTime);


            ATCmd = "AT I 0";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);
            //Console.WriteLine(ATCmd);

            ATCmd = "AT I 13";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);
            //Console.WriteLine(ATCmd);

            ATCmd = "AT+DEL " + "\"" + pgName + "\"" + "+";
            //Console.WriteLine(ATCmd);
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT+FOW " + "\"" + pgName + "\"";
            //Console.WriteLine(ATCmd);
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            SendFileData(Path, port, FormData);

            ATCmd = "AT+FCL";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

            ATCmd = "AT+RUN " + "\"" + pgName + "\"";
            SendSerialData(ATCmd, port, FormData);
            Thread.Sleep(CmdSendTime);

           


        }

       

        public string SendFileData(string Path, SerialPort port, WriteData FormData)
        {
            int ReadMax = 32;
            byte[] ReadByte = new byte[32];
            int readlen = 0;
            string FileData = "";
            FileStream File = new FileStream(Path, FileMode.Open);
            if (File == null)
            {
                return null;
            }
            File.Seek(0, SeekOrigin.Begin);

            while ((readlen = File.Read(ReadByte, 0, ReadMax)) > 0)
            {
                FileData = "AT+FWRH " + "\"" + bytesToHexExt(ReadByte, readlen) + "\"";

                SendSerialData(FileData, port, FormData);
                Thread.Sleep(50);


            }
            File.Close();
            return FileData;
        }
    }
}
