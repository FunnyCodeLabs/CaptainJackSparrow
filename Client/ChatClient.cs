using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ChatClient
    {
        private TCPClient __TcpClient;
        private IPacketProcessorStorage __Processors;

        public ChatClient()
        {
            __Processors = new ClientPacketProcessorStorage(this);
            __TcpClient = new TCPClient(Constants.ServerIPAddress, Constants.PORT, __Processors);

            __TcpClient.ClientConnectionEstablished += ConnectionEstablished;
            __TcpClient.ClientConnectionClosed += ConnectionClosed;
        }

        private void ConnectionClosed(object sender, IConnection closedConnection)
        {
            throw new NotImplementedException();
        }

        private void ConnectionEstablished(object sender, IConnection newConnection)
        {
            throw new NotImplementedException();
        }
    }
}
