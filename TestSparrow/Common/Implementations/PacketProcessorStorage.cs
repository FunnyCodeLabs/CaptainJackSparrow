using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public class PacketProcessorStorage : IPacketProcessorStorage
    {
        private Dictionary<PacketKey, IPacketProcessor> __Storage = new Dictionary<PacketKey, IPacketProcessor>();

        public IPacketProcessor GetProcessor(PacketKey key)
        {
            return __Storage[key];
        }

        public IPacketParser GetParser(PacketKey key)
        {
            return __Storage[key].Parser;
        }

        public IPacketHandler GetHandler(PacketKey key)
        {
            return __Storage[key].Handler;
        }

        public void AddProcessor(IPacketProcessor processor)
        {
            Contract.Requires<ArgumentNullException>(processor != null, "processor");

            if (__Storage.ContainsKey(processor.PacketID))
                return;

            __Storage.Add(processor.PacketID, processor);
        }
    }
}
