using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public class TCPServer: WorkerBase, IServer
    {
        private int __Port;
        private TcpListener __Socket;
        private IPacketProcessorStorage __PacketProcessors;

        private List<IConnection> __ActiveConnections = new List<IConnection>();

        public TCPServer(int port, IPAddress ip,  IPacketProcessorStorage processors)
        {
            __Port = port;
            __Socket = new TcpListener(ip, __Port);
            __PacketProcessors = processors;
        }

        public TCPServer(int port, IPacketProcessorStorage processors)
            : this(port, IPAddress.Any, processors)
        { }

        protected override void JobProc()
        {
            __Socket.Start();

            while (__Stopped)
            {
                TcpClient clientSocket = __Socket.AcceptTcpClient();
                IPacketExchanger tcpExchanger = new TCPDataExchanger(clientSocket, __PacketProcessors);
                IConnection connection = new Connection(tcpExchanger, __PacketProcessors);
                __ActiveConnections.Add(connection);

                connection.Start();
                OnConnectionEstablished(connection);
            }
        }
        public event ConnectionEstablishedEventHandler ConnectionEstablished;

        public ReadOnlyCollection<IConnection> ActiveConnections
        {
            get { return __ActiveConnections.AsReadOnly(); }
        }

        protected virtual void OnConnectionEstablished(IConnection connection)
        {
            ConnectionEstablishedEventHandler handler = ConnectionEstablished;
            if (handler != null)
            {
                handler(this, connection);
            }
        }
    }
}
