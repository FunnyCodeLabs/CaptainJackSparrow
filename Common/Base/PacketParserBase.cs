﻿using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class PacketParserBase: IPacketParser
    {
        protected IDataFormatter __Formatter = new BinaryDataFormatter(sizeof(ushort));

        public abstract IPacket Deserialize(byte[] data);

        public abstract byte[] Serialize(IPacket data);

        protected Exception DeseralizeException(byte[] data)
        {
            if (__Formatter.Cursor < data.Length &&
                data[__Formatter.Cursor] == 0xEE)
            {
                return __Formatter.TakeException(data);
            }
            return null;
        }

        protected void SerializeException(IPacket data)
        {
            if (data.Error != null)
            {
                __Formatter.PutByte(0xEE);
                __Formatter.PutException(data.Error);
            }
        }

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
