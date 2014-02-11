using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public abstract class PacketParserBase: IPacketParser
    {
        private PacketKey __Key;
        private BinaryDataFormatter __Formatter = new BinaryDataFormatter();

        public PacketParserBase(PacketKey id)
        {
            __Key = id;
        }

        public abstract IPacket Deserialize(byte[] data);

        public abstract byte[] Serialize(IPacket data);
    }
}
