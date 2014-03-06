using Communication;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerContext
    {
        private bool __Initialized = false;

        private TCPServer __TcpServer;
        private IInformationManager __Info;
        private IDictionary<string, IConnection> __NickToConnectionMap = new ConcurrentDictionary<string, IConnection>();

        public ServerContext(IInformationManager infoManager)
        {
            __TcpServer = null;
            __Info = infoManager;
        }

        public void Init(TCPServer server)
        {
            __TcpServer = server;
            __Initialized = true;
        }

        #region Public properties

        public bool Initialized
        {
            get
            {
                return __Initialized;
            }
        }

        public TCPServer TcpServer { get { return __TcpServer; } }

        public IInformationManager InfoManager { get { return __Info; } }

        public IDictionary<string, IConnection> NickToConnection { get { return __NickToConnectionMap; } }

        #endregion
    }
}
