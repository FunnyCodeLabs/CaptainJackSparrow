﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public interface IPacketParser
    {
        IPacket Deserialize(byte[] data);
        byte[] Serialize(IPacket data);
    }
}
