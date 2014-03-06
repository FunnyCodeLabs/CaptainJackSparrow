using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public class InformationPacket: AuthPacket
    {
        public new static readonly PacketKey KEY = 0x05;

        public InformationPacket(string nickname, AuthStatus status)
            : base(nickname, status)
        { }

        public InformationPacket(AuthPacket packet)
            : base(packet.Nickname, packet.Status)
        { }
    }
}
