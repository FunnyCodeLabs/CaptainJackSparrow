using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;

namespace Common
{
    public abstract class PacketBase : IPacket
    {
        public static readonly ushort PACKETBASE_SIZE = PacketKey.PacketKeySize;

        protected PacketKey __Key;
        protected Exception __Error = null;

        public PacketBase()
        {
            __Key = new PacketKey(Id);
        }

        public abstract ushort Length { get; }

        public abstract PacketKey Id { get; }

        public Exception Error
        {
            get
            {
                return __Error;
            }
            set
            {
                __Error = value;
            }
        }
    }
}
