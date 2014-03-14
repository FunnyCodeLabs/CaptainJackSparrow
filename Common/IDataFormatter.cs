using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IDataFormatter
    {
        void ResetCursor();
        void ResetBuffer();
        void ResetBuffer(int newBufferSize);
        int Cursor { get; }
        byte[] Buffer { get; }

        Int32 TakeInt32(byte[] data);
        Int16 TakeInt16(byte[] data);
        Int64 TakeInt64(byte[] data);
        Byte TakeByte(byte[] data);
        UInt32 TakeUInt32(byte[] data);
        UInt16 TakeUInt16(byte[] data);
        UInt64 TakeUInt64(byte[] data);
        string TakeString(byte[] data);
        Exception TakeException(byte[] data);

        void PutInt32(Int32 value);
        void PutInt16(Int16 value);
        void PutInt64(Int64 value);
        void PutByte(Byte value);
        void PutUInt32(UInt32 value);
        void PutUInt16(UInt16 value);
        void PutUInt64(UInt64 value);
        void PutString(String value);
        void PutException(Exception e);
    }
}
