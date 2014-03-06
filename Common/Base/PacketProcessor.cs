using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class PacketProcessor: IPacketProcessor
    {
        private IPacketParser __Parser;
        private IPacketHandler __Handler;
        private PacketKey __Key;

        public PacketProcessor(IPacketParser parser, IPacketHandler handler, PacketKey key)
        {
            __Parser = parser;
            __Handler = handler;
            __Key = key;
        }

        public PacketKey PacketID
        {
            get { return __Key; }
        }

        public IPacketParser Parser
        {
            get { return __Parser; }
        }

        public IPacketHandler Handler
        {
            get { return __Handler; }
        }
    }
}
