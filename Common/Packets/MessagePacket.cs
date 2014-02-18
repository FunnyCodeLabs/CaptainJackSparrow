using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    class MessagePacket: MultipleStringContainerPacket
    {
        public static const PacketKey KEY = 0x04;
        public const string TO_EVERYONE_STR = String.Empty;

        public MessagePacket(string from, string to, string message)
            : base(new string[] { from, to, message })
        {
        }

        public string From
        {
            get { return Strings[0]; }
        }

        public string To
        {
            get { return Strings[1]; }
        }

        public string Message
        {
            get { return Strings[2]; }
        }

        public override PacketKey Key
        {
            get
            {
                return KEY;
            }
        }
    }
}
