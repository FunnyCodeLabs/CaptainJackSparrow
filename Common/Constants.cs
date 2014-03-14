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
        internal static readonly PacketKey AuthPacketKey = 0x01;
        internal static readonly PacketKey StringContainerPacketKey = 0x02;
        internal static readonly PacketKey MultipleStringContainerPacketKey = 0x03;
        internal static readonly PacketKey MessagePacketKey = 0x04;
        internal static readonly PacketKey InformationPacketKey = 0x05;
        internal static readonly PacketKey ExceptionPacketKey = 0x06;
    }
}
