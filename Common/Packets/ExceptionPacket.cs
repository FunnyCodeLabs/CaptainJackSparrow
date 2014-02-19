using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    class ExceptionPacket: MultipleStringContainerPacket
    {
        public ExceptionPacket(Exception e)
            : base(new string[] { e.Message, e.StackTrace })
        {
        }

        public ExceptionPacket(string Message, string StackTrace)
            : base(new string[] { Message, StackTrace })
        {
        }

        public string Message
        {
            get { return Strings[0]; }
        }

        public string StackTrace
        {
            get { return Strings[1]; }
        }
    }
}
