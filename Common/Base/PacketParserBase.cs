using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class PacketParserBase: IPacketParser
    {
        protected PacketKey __Key;
        protected IDataFormatter __Formatter = new BinaryDataFormatter(sizeof(ushort));

        public PacketParserBase(PacketKey id)
        {
            __Key = id;
        }

        public abstract virtual IPacket Deserialize(byte[] data);

        public abstract virtual byte[] Serialize(IPacket data);

        protected void GetIDAndLength(byte[] data, out PacketKey id, out ushort length)
        {
            __Formatter.ResetCursor();
            id = __Formatter.TakeInt32(data);
            length = __Formatter.TakeUInt16(data);
        }

        protected void PutIDAndLength(PacketKey id, ushort length)
        {
            __Formatter.PutInt32(id);
            __Formatter.PutUInt16(length);
        }
    }
}
