using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    internal class Constants
    {
        public static readonly PacketKey AuthPacketKey = 0x01;
        public static readonly PacketKey StringContainerPacketKey = 0x02;
        public static readonly PacketKey MultipleStringContainerPacketKey = 0x03;
        public static readonly PacketKey MessagePacketKey = 0x04;
        public static readonly PacketKey InformationPacketKey = 0x05;
    }
}
