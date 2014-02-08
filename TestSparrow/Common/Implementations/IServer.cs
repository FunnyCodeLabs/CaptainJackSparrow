using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public delegate void ConnectionEstablishedEventHandler(object sender, IConnection newConnection);

    public interface IServer: IWorker
    {
        event ConnectionEstablishedEventHandler ConnectionEstablished;
    }
}
