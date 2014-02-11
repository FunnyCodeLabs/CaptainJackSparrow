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
        }

        public override void Stop()
        {
            base.Stop();
            __Connection.Stop();
        }

        public event ConnectionEstablishedEventHandler ConnectionEstablished;
        public event ConnectionClosedEventHandler ConnectionClosed;
    }
}
