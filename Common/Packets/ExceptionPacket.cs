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
            : base(new string[] { e.Message, e.StackTrace, e.GetType().ToString() })
        {
        }

        public ExceptionPacket(string Message, string StackTrace, string ExceptionName)
            : base(new string[] { Message, StackTrace, ExceptionName })
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

        public string ExceptionName
        {
            get { return Strings[2]; }
        }
    }
}
