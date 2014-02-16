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

        private string __From;
        private string __To;
        private string __Message;

        public MessagePacket(string from, string to, string message)
            :base(new List<string>(){from, to, message})
        {
            __From = from;
            __To = to;
            __Message = message;
        }

        public string From
        {
            get { return __From; }
            set { __From = value; }
        }

        public string To
        {
            get { return __To; }
            set { __To = value; }
        }

        public string Message
        {
            get { return __Message; }
            set { __Message = value; }
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
