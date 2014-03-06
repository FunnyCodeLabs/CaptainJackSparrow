using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Parsers
{
    class AuthPacketParser: PacketParserBase
    {
        public override IPacket Deserialize(byte[] data)
        {
            __Formatter.ResetCursor();

            PacketKey key;
            UInt16 length;
            GetIDAndLength(data, out key, out length);

            string nickname = __Formatter.TakeString(data);
            string enumStr = __Formatter.TakeString(data);

            AuthStatus status;
            Enum.TryParse(enumStr, out status);

            return new AuthPacket(nickname, status);
        }

        public override byte[] Serialize(IPacket data)
        {
            AuthPacket packet = data as AuthPacket;
            if (data == null)
                return null;

            __Formatter.ResetCursor();
            __Formatter.ResetBuffer();

            PutIDAndLength(packet.Id, packet.Length);
            __Formatter.PutString(packet.Nickname);

            string enumStr = packet.Status.ToString();
            __Formatter.PutString(enumStr);

            return __Formatter.Buffer;
        }
    }
}
