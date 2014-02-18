using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Parsers
{
    class MultipleStringContainerPacketParser : StringContainerPacketParser
    {
        public static const char DELIMITER = '-';
        public static const char ESCAPE = '\\';

        public override IPacket Deserialize(byte[] data)
        {
            string str = (base.Deserialize(data) as StringContainerPacket).Str;
            String[] multiple = SplitIntoMultiple(str);

            return new MultipleStringContainerPacket(multiple);
        }

        public override byte[] Serialize(IPacket data)
        {
            MultipleStringContainerPacket packet = data as MultipleStringContainerPacket;
            if (data == null)
                return null;

            string oneStr = MakeOne(packet.Strings);
            StringContainerPacket oneStringPacket = new StringContainerPacket(oneStr);

            return base.Serialize(oneStringPacket);
        }

        public static String[] SplitIntoMultiple(string str)
        {
            Regex regex = new Regex(String.Format("(?<!{0}){1}", ESCAPE, DELIMITER));

            String[] multiple = regex.Split(str);
            for (int i = 0; i < multiple.Length; i++)
            {
                multiple[i] = multiple[i].Replace(String.Concat(ESCAPE, DELIMITER), DELIMITER.ToString());
            }

            return multiple;
        }

        public static string MakeOne(String[] multStrings)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < multStrings.Length; i++)
            {
                multStrings[i] = multStrings[i].Replace(DELIMITER.ToString(), (ESCAPE + DELIMITER).ToString());
                b.Append(multStrings[i]);
                b.Append(DELIMITER);
            }
            b.Remove(b.Length - 1, 1);
            return b.ToString();
        }
    }
}
