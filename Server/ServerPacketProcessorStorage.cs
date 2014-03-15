using Common;
using Common.Packets;
using Common.Parsers;
using Server.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ServerPacketProcessorStorage : PacketProcessorStorageBase
    {
        public ServerPacketProcessorStorage(ServerContext context)
        {
            AddProcessor(new PacketProcessor(new AuthPacketParser(), 
                                    new AuthPacketHandler(context),
                                    AuthPacket.KEY));

            AddProcessor(new PacketProcessor(new StringContainerPacketParser(), 
                                    new StringContainerPacketHandler(context),
                                    StringContainerPacket.KEY));

            AddProcessor(new PacketProcessor(new MultipleStringContainerPacketParser(),
                                    new MultipleStringContainerPacketHandler(context),
                                    MultipleStringContainerPacket.KEY));

            AddProcessor(new PacketProcessor(new MessagePacketParser(),
                                    new MessagePacketHandler(context),
                                    MessagePacket.KEY));

            AddProcessor(new PacketProcessor(new AuthInformationPacketParser(),
                                    new AuthInformationPacketHandler(context),
                                    AuthInformationPacket.KEY));
        }
    }
}
