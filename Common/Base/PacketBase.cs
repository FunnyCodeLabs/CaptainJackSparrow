using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;

namespace Common
{
    public abstract class PacketBase : IPacket
    {
        public static const ushort MAX_PACKET_SIZE = UInt16.MaxValue;
        public static const ushort PACKETBASE_SIZE = PacketKey.PacketKeySize;

        protected PacketKey __Key;

        public PacketBase()
        {
            __Key = new PacketKey(Key);
        }

        public abstract virtual PacketKey Key;

        public abstract virtual ushort Length;
    }
}
