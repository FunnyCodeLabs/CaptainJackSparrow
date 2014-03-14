using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public class AuthPacket: StringContainerPacket
    {
        public new static readonly PacketKey KEY = Constants.AuthPacketKey;
        public static readonly int MAX_NICKNAME_LENGTH = StringContainerPacket.MAX_STRING_LENGTH - sizeof(AuthStatus);

        private readonly AuthStatus __Status;

        public AuthPacket(string nickname, AuthStatus status)
            :base(nickname)
        {
            __Status = status;
        }

        public string Nickname
        {
            get { return __Str; }
        }

        public AuthStatus Status
        {
            get { return __Status; }
        }

        public override ushort Length
        {
            get
            {
                return (ushort)(base.Length + sizeof(AuthStatus));
            }
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
