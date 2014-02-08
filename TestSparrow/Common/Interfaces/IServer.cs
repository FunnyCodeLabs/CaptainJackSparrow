using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public delegate void ConnectionEstablishedEventHandler(object sender, IConnection newConnection);
    public delegate void ConnectionClosedEventHandler(object sender, IConnection closedConnection);

    public interface IServer: IWorker
    {
        event ConnectionEstablishedEventHandler ServerConnectionEstablished;
        event ConnectionClosedEventHandler ServerConnectionClosed;
        ReadOnlyCollection<IConnection> ActiveConnections { get; }
    }
}
