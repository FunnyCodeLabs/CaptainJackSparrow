using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow
{
    class EncDec : ICrypto
    {
        static string Encode(string text)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char item in text.ToCharArray())
            {
                foreach (byte b in BitConverter.GetBytes(item).Reverse())
                {
                    builder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }
            }

            return builder.ToString();
        }

        static string Decode(string binaryString)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < binaryString.Length; i += sizeof(char) * 8)
            {
                string str = binaryString.Substring(i, sizeof(char) * 8);
                builder.Append((char)Convert.ToInt16(str, 2));
            }

            return builder.ToString();
        }

        string ICrypto.Encode(string s)
        {
            return EncDec.Encode(s);
        }

        string ICrypto.Decode(string s)
        {
            return EncDec.Decode(s);
        }
    }
}
