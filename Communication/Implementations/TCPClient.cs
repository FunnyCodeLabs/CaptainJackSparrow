using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class TCPClient : WorkerBase, IClient
    {
        private IPacketProcessorStorage __Processors;
        private IConnection __Connection = null;
        
        private TcpClient __Socket;
        private IPAddress __Ip;
        private int __Port;

        public TCPClient(IPAddress ip, int port, IPacketProcessorStorage processors)
        {
            __Socket = new TcpClient();
            __Ip = ip;
            __Port = port;

            __Processors = processors;
        }

        public override void Start()
        {
            base.Start();
            __Socket.Connect(new IPEndPoint(__Ip, __Port));
            __Connection = new Connection(new TCPPacketExchanger(__Socket, __Processors), __Processors);
            __Connection.Start();
            __Connection.ConnectionClosed += __Connection_ConnectionClosed;
            OnClientConnectionEstablished(__Connection);
        }

        private void __Connection_ConnectionClosed(object sender, IConnection closedConnection)
        {
            OnClientConnectionClosed(closedConnection);
        }

        public override void Stop()
        {
            base.Stop();
            __Connection.Stop();
            OnClientConnectionClosed(__Connection);
        }

        protected virtual void OnClientConnectionEstablished(IConnection connection)
        {
            ConnectionEstablishedEventHandler handler = ClientConnectionEstablished;
            if (handler != null)
            {
                handler(this, connection);
            }
        }
        protected virtual void OnClientConnectionClosed(IConnection connection)
        {
            ConnectionClosedEventHandler handler = ClientConnectionClosed;
            if (handler != null)
            {
                handler(this, connection);
            }
        }

        public event ConnectionEstablishedEventHandler ClientConnectionEstablished;
        public event ConnectionClosedEventHandler ClientConnectionClosed;
    }
}
