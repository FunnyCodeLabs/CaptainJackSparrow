using Communication;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class PacketProcessorStorageBase: IPacketProcessorStorage
    {
        private ConcurrentDictionary<PacketKey, IPacketProcessor> __Storage;

        public PacketProcessorStorageBase()
        {
            __Storage = new ConcurrentDictionary<PacketKey, IPacketProcessor>();
        }

        protected void AddProcessor(IPacketProcessor processor)
        {
            __Storage.TryAdd(processor.PacketID, processor);
        }

        protected IPacketProcessor TryGet(PacketKey key)
        {
            IPacketProcessor processor;
            __Storage.TryGetValue(key, out processor);
            return processor;
        }

        public virtual IPacketProcessor GetProcessor(PacketKey key)
        {
            return TryGet(key);
        }

        public virtual IPacketHandler GetHandler(PacketKey key)
        {
            return TryGet(key).Handler;
        }

        public virtual IPacketParser GetParser(PacketKey key)
        {
            return TryGet(key).Parser;
        }
    }
}
