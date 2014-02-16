using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    class AuthPacket: StringContainerPacket
    {
        public static const int MAX_NICKNAME_LENGTH = StringContainerPacket.MAX_STRING_LENGTH - sizeof(AuthStatus);

        private string __Nickname;
        private AuthStatus __Status;

        public AuthPacket(string nickname, AuthStatus status)
            :base(nickname)
        {
            if (nickname.Length > MAX_NICKNAME_LENGTH)
                __Nickname = new String(nickname.Take(MAX_NICKNAME_LENGTH).ToArray());
            else
                __Nickname = nickname;

            __Status = status;
        }

        public string Nickname
        {
            get { return __Nickname; }
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

        public override int ID
        {
            get
            {
                return 0x01;
            }
        }
    }
}
