using Common;
using Common.Packets;
using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ChatServer: WorkerBase
    {
        private TCPServer __TcpServer;
        private IInformationManager __Info;
        private ServerContext __Context;

        private IPacketProcessorStorage __Processors;

        public ChatServer(IInformationManager infoManager)
        {
            __Info = infoManager;

            __Context = new ServerContext(infoManager);

            __Processors = new ServerPacketProcessorStorage(__Context);
            __TcpServer = new TCPServer(Constants.PORT, __Processors);
            __TcpServer.ServerConnectionEstablished += ServerConnectionEstablished;
            __TcpServer.ServerConnectionClosed += ServerConnectionClosed;

            __Context.Init(__TcpServer);
        }

        protected override void JobProc()
        {
            __TcpServer.Run();
        }

        private void ServerConnectionClosed(object sender, IConnection closedConnection)
        {
            Logger.FileLogger.Write("Connection closed. Hashcode: " + closedConnection.GetHashCode());
        }

        private void ServerConnectionEstablished(object sender, IConnection newConnection)
        {
            Logger.FileLogger.Write("New connection established. Hashcode: " + newConnection.GetHashCode());
        }
    }
}
