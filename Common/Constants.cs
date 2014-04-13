using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constants
    {
        public const int PORT = 3332;
        public static readonly IPAddress ServerIPAddress = new IPAddress(new byte[4]);

        public static readonly PacketKey AuthPacketKey = 0x01;
        public static readonly PacketKey StringContainerPacketKey = 0x02;
        public static readonly PacketKey MultipleStringContainerPacketKey = 0x03;
        public static readonly PacketKey MessagePacketKey = 0x04;
        public static readonly PacketKey InformationPacketKey = 0x05;
    }
}
