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
        public static readonly PacketKey KEY = Constants.StringContainerPacketKey;

        protected readonly string __Str;

        public StringContainerPacket(string str)
        {
            __Str = str;
        }

        public override ushort Length
        {
            get
            {
                return (ushort)(PACKETBASE_SIZE + __Str.Length * sizeof(char));
            }
        }

        public string Str
        {
            get
            {
                return __Str;
            }
        }

        public override PacketKey Id
        {
            get
            {
                return KEY;
            }
        }
    }
}
