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
        public StringContainerPacketParser(IDataFormatter __Formatter)
            : base(StringContainerPacket.KEY)
        { }

        public override IPacket Deserialize(byte[] data)
        {
            __Formatter.ResetCursor();

            PacketKey key;
            UInt16 length;
            GetIDAndLength(data, out key, out length);

            UInt16 strLength = __Formatter.TakeUInt16(data);
            string str = __Formatter.TakeString(data, length);

            return new StringContainerPacket(str);
        }

        public override byte[] Serialize(StringContainerPacket data)
        {
            __Formatter.ResetCursor();
            __Formatter.ResetBuffer();

            PutIDAndLength(data.Key, data.Length);
            __Formatter.PutString(data.Str);

            return __Formatter.Buffer;
        }
    }
}
