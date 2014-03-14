using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Parsers
{
    public class MessagePacketParser: MultipleStringContainerPacketParser
    {
        public override byte[] Serialize(IPacket data)
        {
            MessagePacket packet = data as MessagePacket;
            if (data == null)
                return null;

            MultipleStringContainerPacket multStrings = new MultipleStringContainerPacket(
                new string[] {
                    packet.From,
                    packet.To,
                    packet.Message
                });

            return base.Serialize(multStrings);
        }

        public override IPacket Deserialize(byte[] data)
        {
            MultipleStringContainerPacket multStrings = (MultipleStringContainerPacket)base.Deserialize(data);

            MessagePacket p = new MessagePacket(multStrings.Strings[0], multStrings.Strings[1], multStrings.Strings[2]);
            Exception e = DeseralizeException(data);
            if (e != null)
                p.Error = e;
            return p;
        }
    }
}
