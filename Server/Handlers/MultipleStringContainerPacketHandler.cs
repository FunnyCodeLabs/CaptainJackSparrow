﻿using Common;
using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Handlers
{
    internal class MultipleStringContainerPacketHandler: ServerPacketHandler
    {
        public MultipleStringContainerPacketHandler(ServerContext context)
            : base(context)
        { }

        public override void Handle(IConnection sender, IPacket packet)
        {
            Logger.FileLogger.WriteError(String.Format("Standalone MultipleStringContainerPacket! Packet {0}. Sender: {1}",
                packet.ToString(), sender.ToString()));
        }
    }
}
