using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public class TCPServer: WorkerBase
    {
        private int __Port;
        private TcpListener __Socket;
        private IPacketProcessorStorage __PacketProcessors;

        private List<Connection> __ActiveConnections = new List<Connection>();

        public TCPServer(int port, IPacketProcessorStorage processors)
        {
            __Port = port;
            __Socket = new TcpListener(IPAddress.Any, __Port);
        }

        protected override void JobProc()
        {
            __Socket.Start();

            while (__Stopped)
            {
                TcpClient clientSocket = __Socket.AcceptTcpClient();
                IDataExchanger tcpExchanger = new TCPDataExchanger(clientSocket);
                Connection connection = new Connection(tcpExchanger, __PacketProcessors);
                __ActiveConnections.Add(connection);

                connection.Start();
            }
        }
    }
}
