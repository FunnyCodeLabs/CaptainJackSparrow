using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ClientPacketProcessorStorage : IPacketProcessorStorage
    {
        public ClientPacketProcessorStorage(ChatClient client)
        {

        }

        public IPacketProcessor GetProcessor(PacketKey key)
        {
            throw new NotImplementedException();
        }

        public IPacketHandler GetHandler(PacketKey key)
        {
            throw new NotImplementedException();
        }

        public IPacketParser GetParser(PacketKey key)
        {
            throw new NotImplementedException();
        }
    }
}
