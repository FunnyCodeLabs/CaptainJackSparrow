using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Packets;
using Communication;

namespace Common.Parsers
{
    class StringContainerPacketParser: PacketParserBase
    {
        public override IPacket Deserialize(byte[] data)
        {
            __Formatter.ResetCursor();

            PacketKey key;
            UInt16 length;
            GetIDAndLength(data, out key, out length);

            string str = __Formatter.TakeString(data);

            return new StringContainerPacket(str);
        }

        public override byte[] Serialize(IPacket data)
        {
            StringContainerPacket packet = data as StringContainerPacket;
            if (data == null)
                return null;

            __Formatter.ResetCursor();
            __Formatter.ResetBuffer();

            PutIDAndLength(packet.Id, packet.Length);
            __Formatter.PutString(packet.Str);

            return __Formatter.Buffer;
        }
    }
}
