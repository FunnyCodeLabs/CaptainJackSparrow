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
    public class TCPDataExchanger : WorkerBase, IDataExchanger
    {
        private const int HEADER_SIZE = sizeof(Int32) + sizeof(UInt16);

        private TcpClient __Socket;
        private NetworkStream __Stream;

        private bool __Stopped = false;

        private Task __InputProcessorThread;
        private byte[] __HeaderBuffer;
        private byte[] __DataBuffer;

        private Task __OutputProcessorThread;
        private Queue<byte[]> __OutputQueue = new Queue<byte[]>();
        private EventWaitHandle __OutputMonitor = new ManualResetEvent(false);

        public TCPDataExchanger(TcpClient client)
        {
            __Socket = client;
            __Stream = __Socket.GetStream();
        }

        protected override void JobProc()
        {
            __InputProcessorThread = Task.Factory.StartNew(InputProcessor);
            __OutputProcessorThread = Task.Factory.StartNew(OutputProcessor);
        }

        private void InputProcessor()
        {
            BinaryReader tcpReader = new BinaryReader(__Stream);

            while (!__Stopped)
            {
                Int32 id = ReadHeaderAndData(tcpReader);

                if (__Stopped)
                    return;

                PacketKey key = new PacketKey(id);

                var data = new byte[__HeaderBuffer.Length + __DataBuffer.Length];
                __HeaderBuffer.CopyTo(data, 0);
                __DataBuffer.CopyTo(data, __HeaderBuffer.Length);

                if (__Stopped)
                    return;

                OnDataRecieved(data);
            }
        }

        private void OutputProcessor()
        {
            BinaryWriter tcpWriter = new BinaryWriter(__Stream);

            while (!__Stopped)
            {
                __OutputMonitor.WaitOne();

                if (__OutputQueue.Count > 0)
                    tcpWriter.Write(__OutputQueue.Dequeue());
                else
                    __OutputMonitor.Reset();
            }
        }

        private int ReadHeaderAndData(BinaryReader reader)
        {
            int RB = reader.Read(__HeaderBuffer, 0, HEADER_SIZE);
            if (RB != HEADER_SIZE)
            {
                __Stopped = true;
                return -1;
            }

            Int32 id = BitConverter.ToInt32(__HeaderBuffer, 0);
            UInt16 length = BitConverter.ToUInt16(__HeaderBuffer, sizeof(Int32));

            RB = reader.Read(__DataBuffer, 0, length);
            if (RB != HEADER_SIZE)
            {
                __Stopped = true;
                return -1;
            }

            return id;
        }

        protected virtual void OnDataRecieved(byte[] data)
        {
            DataRecievedEventHandler handler = DataRecieved;
            if (handler != null)
            {
                handler(data);
            }
        }

        #region IDataExchanger

        public event DataRecievedEventHandler DataRecieved;

        public void Send(byte[] data)
        {
            __OutputQueue.Enqueue(data);
            __OutputMonitor.Set();
        }

        #endregion
    }
}
