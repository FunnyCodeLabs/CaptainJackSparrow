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

        public ServerPacketHandler(PacketKey key, ServerContext context)
            :base(key)
        {
            __Context = context;
        }
    }
}
