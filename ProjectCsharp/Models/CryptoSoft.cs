using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace test2
{
    public class CryptoSoft
    {
        public string source;
        public string dest;
        static private char[] passcode
        {
            get { return "GGEZ".ToCharArray(); }
        }
        public void Cryptage(String sourcePATH,String destPATH)
        {

            byte[] filesource = File.ReadAllBytes(sourcePATH);

            File.WriteAllBytes(destPATH, xor(filesource));

        }
        static private byte[] xor(byte[] source)
        {
            byte[] result = new byte[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = (byte)(source[i] ^ ((byte)passcode[i % passcode.Length]));

            }
            return result;
        }
    }
}