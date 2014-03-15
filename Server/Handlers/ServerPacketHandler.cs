using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Handlers
{
    internal abstract class ServerPacketHandler : PacketHandlerBase
    {
        protected ServerContext __Context;

        public ServerPacketHandler(ServerContext context)
        {
            __Context = context;
        }

        protected virtual void NotifyClients(IConnection sender, IPacket packet)
        {
            foreach (var item in __Context.ClientToConnection.Values)
            {
                if (item != sender)
                {
                    item.Send(packet);
                }
            }
        }
    }
}
