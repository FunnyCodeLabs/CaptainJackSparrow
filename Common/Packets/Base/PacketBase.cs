using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;

namespace Common.Packets
{
    public abstract class PacketBase : IPacket
    {
        private PacketKey __Key;
        private UInt16 __Length;
        private byte[] __Data;

        public PacketBase(int id)
            : this(id, null)
        { 

        }

        public PacketBase(int id, byte[] data)
        {
            __Key = new PacketKey(id);
            __Data = data;
            UpdateLength();
        }

        protected void UpdateLength()
        {
            __Length = (ushort)(sizeof(Int32) + sizeof(UInt16) + (__Data != null ? __Data.Length : 0));
        }

        public virtual byte[] Data
        {
            get
            {
                return __Data;
            }
            set
            {
                __Data = value;
                UpdateLength();
            }
        }

        public virtual PacketKey Id
        {
            get { return __Key; }
        }

        public virtual ushort Length
        {
            get 
            {
                return __Length;
            }
        }
    }
}
