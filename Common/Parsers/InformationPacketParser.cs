using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Parsers
{
    public class AuthInformationPacketParser: AuthPacketParser
    {
        public override byte[] Serialize(IPacket data)
        {
            return base.Serialize(data);
        }

        public override IPacket Deserialize(byte[] data)
        {
            AuthPacket packet = base.Deserialize(data) as AuthPacket;
            return new AuthInformationPacket(packet);
        }
    }
}
