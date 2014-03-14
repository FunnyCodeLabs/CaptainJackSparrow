﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public interface IPacket
    {
        PacketKey Id { get; }
        ushort Length { get; }
        Exception Error { get; set; }
    }
}
