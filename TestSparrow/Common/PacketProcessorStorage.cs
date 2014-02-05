using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    class PacketProcessorStorage : IPacketProcessorStorage
    {
        private Dictionary<PacketKey, IPacketProcessor> __Storage;

        public IPacketProcessor GetProcessor(PacketKey key)
        {
            return __Storage[key];
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
