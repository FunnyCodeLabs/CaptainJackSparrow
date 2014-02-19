using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Parsers
{
    class ExceptionPacketParser: MultipleStringContainerPacketParser
    {
        public override byte[] Serialize(IPacket data)
        {
            ExceptionPacket packet = data as ExceptionPacket;
            if (data == null)
                return null;

            return base.Serialize(packet);
        }

        public override IPacket Deserialize(byte[] data)
        {
            MultipleStringContainerPacket multPacket = (MultipleStringContainerPacket)base.Deserialize(data);

            return new ExceptionPacket(multPacket.Strings[0], multPacket.Strings[1], multPacket.Strings[2]);
        }
    }
}
