using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public class InformationPacket: StringContainerPacket
    {
        public new static readonly PacketKey KEY = 0x05;

        private readonly ushort[] __Highlights;

        public InformationPacket(string info, ushort[] highlights)
            :base(info)
        {
            __Highlights = highlights;
        }

        public string Info
        {
            get { return Str; }
        }

        public ushort[] Highlights
        {
            get { return __Highlights; }
        }

        public override PacketKey Id
        {
            get
            {
                return KEY;
            }
        }
    }
}
