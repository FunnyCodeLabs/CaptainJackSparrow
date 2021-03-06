﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class TCPServer : WorkerBase, IServer
    {
        private int __Port;
        private TcpListener __ListenerSocket;
        private IPacketProcessorStorage __PacketProcessors;

        private List<IConnection> __ActiveConnections = new List<IConnection>();
        private object __ConnectionsLock = new object();

        public TCPServer(int port, IPAddress ip, IPacketProcessorStorage processors)
        {
            __Port = port;
            __ListenerSocket = new TcpListener(ip, __Port);
            __PacketProcessors = processors;

        }

        public TCPServer(int port, IPacketProcessorStorage processors)
            : this(port, IPAddress.Any, processors)
        { }

        private void ConnectionClosed_EventHandler(object sender, IConnection connection)
        {
            lock (__ConnectionsLock)
            {
                __ActiveConnections.Remove(connection);
            }
            OnServerConnectionClosed(connection);
        }

        protected override void JobProc()
        {
            __ListenerSocket.Start();

            while (!__Stopped)
            {
                TcpClient clientSocket = __ListenerSocket.AcceptTcpClient();
                IPacketExchanger tcpExchanger = new TCPPacketExchanger(clientSocket, __PacketProcessors);
                IConnection connection = new Connection(tcpExchanger, __PacketProcessors);
                lock (__ConnectionsLock)
                    __ActiveConnections.Add(connection);
                connection.ConnectionClosed += ConnectionClosed_EventHandler;

                connection.Start();
                OnServerConnectionEstablished(connection);
            }
        }

        protected virtual void OnServerConnectionEstablished(IConnection connection)
        {
            ConnectionEstablishedEventHandler handler = ServerConnectionEstablished;
            if (handler != null)
            {
                handler(this, connection);
            }
        }
        protected virtual void OnServerConnectionClosed(IConnection connection)
        {
            ConnectionClosedEventHandler handler = ServerConnectionClosed;
            if (handler != null)
            {
                handler(this, connection);
            }
        }

        public event ConnectionEstablishedEventHandler ServerConnectionEstablished;
        public event ConnectionClosedEventHandler ServerConnectionClosed;
        public ReadOnlyCollection<IConnection> ActiveConnections
        {
            get 
            {
                lock (__ConnectionsLock)
                    return __ActiveConnections.AsReadOnly();
            }
        }
    }
}
