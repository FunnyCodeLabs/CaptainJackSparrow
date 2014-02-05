using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    interface IPacket
    {
        PacketKey Id { get; }
        uint Length { get; }
    }
}
