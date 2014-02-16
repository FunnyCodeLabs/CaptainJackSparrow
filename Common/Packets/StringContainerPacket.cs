using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public class StringContainerPacket: PacketBase
    {
        public static const int MAX_STRING_LENGTH = MAX_PACKET_SIZE / sizeof(char);

        private string __Str;

        public StringContainerPacket(string str)
        {
            if (str.Length > MAX_STRING_LENGTH)
                __Str = new String(str.Take(MAX_STRING_LENGTH).ToArray());
            else
                __Str = str;
        }

        public String Str
        {
            get { return __Str; }
        }

        public virtual ushort Length
        {
            get
            {
                return (ushort)(PACKETBASE_SIZE + __Str.Length * sizeof(char));
            }
        }

        public override int ID
        {
            get { return 0x02; }
        }
    }
}
