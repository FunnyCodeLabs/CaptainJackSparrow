using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class UserClient
    {
        private string __Nickname;
        private AuthStatus __CurrentStatus;

        public UserClient(string nick, AuthStatus status)
        {
            __Nickname = nick;
            __CurrentStatus = status;
        }

        public string Nickname
        {
            get
            {
                return __Nickname;
            }
        }

        public AuthStatus Status
        {
            get
            {
                return __CurrentStatus;
            }
            set
            {
                __CurrentStatus = value;
            }
        }
    }
}
