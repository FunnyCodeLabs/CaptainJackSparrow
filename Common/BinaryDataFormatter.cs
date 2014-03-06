using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BinaryDataFormatter : IDataFormatter
    {
        public static readonly Encoding StringEncoding = Encoding.Unicode;

        private int __BufferSize;
        private int __Cursor;
        private byte[] __Buffer;

        public BinaryDataFormatter(int bufferSize)
        {
            __BufferSize = bufferSize;
            __Cursor = 0;
            __Buffer = new byte[__BufferSize];
        }

        public void ResetBuffer()
        {
            __Buffer = new byte[__BufferSize];
        }

        public void ResetBuffer(int newBufferSize)
        {
            __Buffer = new byte[newBufferSize];
            __BufferSize = newBufferSize;
        }

        public void ResetCursor()
        {
            __Cursor = 0;
        }

        public byte[] Buffer
        {
            get { return __Buffer; }
        }

        public int Cursor
        {
            get { return __Cursor; }
        }

        public int TakeInt32(byte[] data)
        {
            var v = BitConverter.ToInt32(data, __Cursor);
            __Cursor += sizeof(Int32);
            return v;
        }

        public short TakeInt16(byte[] data)
        {
            var v = BitConverter.ToInt16(data, __Cursor);
            __Cursor += sizeof(Int16);
            return v;
        }

        public long TakeInt64(byte[] data)
        {
            var v = BitConverter.ToInt64(data, __Cursor);
            __Cursor += sizeof(Int64);
            return v;
        }

        public byte TakeByte(byte[] data)
        {
            var v = data[__Cursor];
            __Cursor += sizeof(Byte);
            return v;
        }

        public uint TakeUInt32(byte[] data)
        {
            var v = BitConverter.ToUInt32(data, __Cursor);
            __Cursor += sizeof(UInt32);
            return v;
        }

        public ushort TakeUInt16(byte[] data)
        {
            var v = BitConverter.ToUInt16(data, __Cursor);
            __Cursor += sizeof(UInt16);
            return v;
        }

        public ulong TakeUInt64(byte[] data)
        {
            var v = BitConverter.ToUInt64(data, __Cursor);
            __Cursor += sizeof(UInt64);
            return v;
        }

        public string TakeString(byte[] data)
        {
            UInt16 length = this.TakeUInt16(data);

            var v = BitConverter.ToString(data, __Cursor, length);
            __Cursor += sizeof(char) * length;
            return v;
        }

        private void PutInBuffer(byte[] data)
        {
            Array.Copy(data, 0, __Buffer, __Cursor, data.Length);
            __Cursor += data.Length;
        }

        public void PutInt32(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutInt16(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutInt64(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutByte(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutUInt32(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutUInt16(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutUInt64(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            PutInBuffer(bytes);
        }

        public void PutString(string value)
        {
            byte[] bytes = StringEncoding.GetBytes(value);
            byte[] bytesLength = BitConverter.GetBytes((UInt16)value.Length);

            PutInBuffer(bytesLength);
            PutInBuffer(bytes);
        }
    }
}
