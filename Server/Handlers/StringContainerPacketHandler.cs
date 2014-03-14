﻿using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Handlers
{
    internal class StringContainerPacketHandler : ServerPacketHandler
    {
        public StringContainerPacketHandler(PacketKey key, ServerContext context)
            : base(key, context)
        { }

        public override void Handle(IConnection sender, IPacket packet)
        {
            Logger.FileLogger.WriteError(String.Format("Standalone StringContainerPacket! Packet {0}. Sender: {1}",
                packet.ToString(), sender.ToString()));
        }
    }
}
