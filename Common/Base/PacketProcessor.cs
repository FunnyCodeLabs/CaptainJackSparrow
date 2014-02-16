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
        public class PacketProcessorFactory
        {
            public PacketProcessorFactory()
            { }

            public PacketProcessor GetByKey(PacketKey key)
            {
                throw new NotImplementedException();
            }
        }

        public static PacketProcessorFactory Factory
        {
            get
            {
                if (__Instance == null)
                {
                    Monitor.Enter(__SLock);
                    if (__Instance == null)
                        __Instance = new PacketProcessorFactory();
                    Monitor.Exit(__SLock);
                }
                return __Instance;
            }
        }
        private static PacketProcessorFactory __Instance;
        private static readonly Object __SLock = new Object();

        private IPacketParser __Parser;
        private IPacketHandler __Handler;
        private PacketKey __Key;

        private PacketProcessor(IPacketParser parser, IPacketHandler handler, PacketKey key)
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
