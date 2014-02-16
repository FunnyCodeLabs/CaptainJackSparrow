using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;

namespace Common.Packets
{
    public abstract class PacketBase : IPacket
    {
        public static const ushort MAX_PACKET_SIZE = UInt16.MaxValue;
        public static const ushort PACKETBASE_SIZE = PacketKey.PacketKeySize;

        private PacketKey __Key;

        public PacketBase()
        {
            __Key = new PacketKey(ID);
        }

        protected abstract virtual int ID;

        public PacketKey Key
        {
            get { return __Key; }
        }

        public abstract virtual ushort Length;
    }
}
