using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public class MessagePacket: MultipleStringContainerPacket
    {
        public new static readonly PacketKey KEY = Constants.MessagePacketKey;
        public static readonly string TO_EVERYONE_STR = String.Empty;

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

        public override PacketKey Id
        {
            get
            {
                return KEY;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}. Message: {1}. From: {2}. To: {3}", GetType(), Message, From, To);
        }
    }
}
