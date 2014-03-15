using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class PacketHandlerBase: IPacketHandler
    {
        public abstract void Handle(IConnection sender, IPacket packet);
    }
}
