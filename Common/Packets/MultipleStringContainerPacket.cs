using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    class MultipleStringContainerPacket: StringContainerPacket
    {
        public static const PacketKey KEY = 0x03;
        public static const char DELIMITER = '-';

        private String[] __MultStrings;

        public MultipleStringContainerPacket(IEnumerable<string> multStrings)
            :base(MakeOne(multStrings))
        {
            __MultStrings = multStrings.ToArray();
        }

        public String[] Strings
        {
            get
            {
                return __MultStrings;
            }
        }

        public static string MakeOne(IEnumerable<string> multStrings)
        {
            StringBuilder b = new StringBuilder();
            foreach (var s in multStrings)
            {
                b.Append(s);
                b.Append(DELIMITER);
            }
            b.Remove(b.Length - 1, 1);
            return b.ToString();
        }

        public override PacketKey Key
        {
            get
            {
                return KEY;
            }
        }
    }
}
