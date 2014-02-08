using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public class TCPPacketExchanger : WorkerBase, IPacketExchanger
    {
        private const int HEADER_SIZE = sizeof(Int32) + sizeof(UInt16);
        public const int STOP_TIMEOUT = 1000;

        private TcpClient __Socket;
        private NetworkStream __Stream;

        private IPacketParserStorage __KeyToParserMap;

        private Task __InputProcessorThread;
        private byte[] __PacketBuffer = null;
        private Int32 __PacketID;
        private UInt16 __PacketLength;

        private Task __OutputProcessorThread;
        private Queue<IPacket> __OutputQueue = new Queue<IPacket>();
        private EventWaitHandle __OutputMonitor = new ManualResetEvent(false);

        public TCPPacketExchanger(TcpClient connectedClient, IPacketParserStorage parsers)
        {
            __Socket = connectedClient;
            __Stream = __Socket.GetStream();
            __KeyToParserMap = parsers;
        }

        protected override void JobProc()
        {
            __InputProcessorThread = Task.Factory.StartNew(InputProcessor);
            __OutputProcessorThread = Task.Factory.StartNew(OutputProcessor);
        }

        protected virtual void OnPacketRecieved(IPacket packet)
        {
            DataRecievedEventHandler handler = DataRecieved;
            if (handler != null)
            {
                handler(packet);
            }
        }

        private void InputProcessor()
        {
            BinaryReader tcpReader = new BinaryReader(__Stream);

            while (!__Stopped)
            {
                int RB = 0;
                RB = ReadPacketData(tcpReader);

                if (__Stopped)
                    return;

                PacketKey key = new PacketKey(__PacketID);
                IPacketParser parser = __KeyToParserMap.GetParser(key);
                IPacket packet = parser.Deserialize(__PacketBuffer);

                OnPacketRecieved(packet);
            }
        }

        private void OutputProcessor()
        {
            BinaryWriter tcpWriter = new BinaryWriter(__Stream);

            while (!__Stopped)
            {
                __OutputMonitor.WaitOne();

                if (__OutputQueue.Count > 0)
                {
                    IPacket packet = __OutputQueue.Dequeue();
                    IPacketParser parser = __KeyToParserMap.GetParser(packet.Id);
                    byte[] data = parser.Serialize(packet);

                    tcpWriter.Write(data, 0, packet.Length);
                }
                else
                    __OutputMonitor.Reset();
            }
        }

        private int ReadPacketData(BinaryReader reader)
        {
            int RB = reader.Read(__PacketBuffer, 0, HEADER_SIZE);
            if (RB != HEADER_SIZE)
            {
                __Stopped = true;
                return RB;
            }

            __PacketID = BitConverter.ToInt32(__PacketBuffer, 0);
            __PacketLength = BitConverter.ToUInt16(__PacketBuffer, sizeof(Int32));

            RB = reader.Read(__PacketBuffer, HEADER_SIZE, __PacketLength - HEADER_SIZE);
            if (RB != __PacketLength - HEADER_SIZE)
            {
                __Stopped = true;
                return RB;
            }

            return RB;
        }

        public override void Stop()
        {
            base.Stop();
            Task.WaitAll(new Task[2] { __InputProcessorThread, __OutputProcessorThread }, STOP_TIMEOUT);

            __Socket.Close();
        }

        #region IPacketExchanger

        public event DataRecievedEventHandler DataRecieved;

        public void Send(IPacket packet)
        {
            __OutputQueue.Enqueue(packet);
            __OutputMonitor.Set();
        }

        public bool Active
        {
            get 
            {
                try
                {
                    return !(__Socket.Client.Poll(1, SelectMode.SelectRead) && __Socket.Client.Available == 0);
                }
                catch (SocketException) 
                { 
                    return false; 
                }
            }
        }

        #endregion
    }
}
