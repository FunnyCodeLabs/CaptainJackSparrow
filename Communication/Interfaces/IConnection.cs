using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public interface IConnection: IWorker
    {
        void Send(IPacket packet);
        IPacketExchanger Exchanger { get; }

        event ConnectionClosedEventHandler ConnectionClosed;
    }
}
