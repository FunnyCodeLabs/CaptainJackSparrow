﻿using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Packets
{
    public abstract class PacketHandlerBase: IPacketHandler
    {
        private PacketKey __Key;

        public PacketHandlerBase(PacketKey id)
        {
            __Key = id;
        }

        public abstract void Handle(IPacket packet);
    }
}
