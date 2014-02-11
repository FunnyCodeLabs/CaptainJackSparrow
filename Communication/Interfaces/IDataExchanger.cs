using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public delegate void DataRecievedEventHandler(IPacket packet);

    public interface IPacketExchanger: IWorker
    {
        void Send(IPacket packet);
        event DataRecievedEventHandler DataRecieved;
        bool Active { get; }
    }
}
