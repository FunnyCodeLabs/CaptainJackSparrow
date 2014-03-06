using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Parsers
{
    class InformationPacketParser: PacketParserBase
    {
        public override byte[] Serialize(IPacket data)
        {
            InformationPacket packet = data as InformationPacket;
            if (data == null)
                return null;

            __Formatter.ResetBuffer();
            __Formatter.ResetCursor();

            PutIDAndLength(packet.Id, packet.Length);

            __Formatter.PutString(packet.Info);

            __Formatter.PutUInt16((UInt16)packet.Highlights.Length);
            for (int i = 0; i < packet.Highlights.Length; i++)
            {
                __Formatter.PutUInt16(packet.Highlights[i]);
            }

            return __Formatter.Buffer;
        }

        public override IPacket Deserialize(byte[] data)
        {
            __Formatter.ResetCursor();

            PacketKey key;
            UInt16 length;
            GetIDAndLength(data, out key, out length);

            string info = __Formatter.TakeString(data);

            ushort arrayLength = __Formatter.TakeUInt16(data);
            UInt16[] array = new UInt16[arrayLength];

            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = __Formatter.TakeUInt16(data);
            }

            return new InformationPacket(info, array);
        }
    }
}
