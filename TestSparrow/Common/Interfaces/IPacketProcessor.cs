﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    interface IPacketProcessor
    {
        PacketKey PacketID { get; }
        IPacketParser Parser { get; }
        IPacketHandler Handler { get; }
    }
}
