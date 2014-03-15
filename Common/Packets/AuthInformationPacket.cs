using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public class AuthInformationPacket: AuthPacket
    {
        public new static readonly PacketKey KEY = Constants.InformationPacketKey;

        public AuthInformationPacket(string nickname, AuthStatus status)
            : base(nickname, status)
        { }

        public AuthInformationPacket(AuthPacket packet)
            : base(packet.Nickname, packet.Status)
        {
            Error = packet.Error;
        }

        public override string ToString()
        {
            return String.Format("{0}. Nick: {1}. Status: {2}.", GetType(), Nickname, Status);
        }
    }
}
