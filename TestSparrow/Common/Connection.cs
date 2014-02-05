using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public abstract class Connection
    {
        private IDataExchanger __Exchanger;
        private IPacketProcessorStorage __PacketProcessorStorage;

        private List<Task> __RunningHandles = new List<Task>();

        public Connection(IDataExchanger exchanger, IPacketProcessorStorage storage)
        {
            __Exchanger = exchanger;
            __PacketProcessorStorage = storage;
        }

        private void DataRecievedAsync(byte[] data)
        {
            var task = Task.Factory.StartNew(() =>
                {
                    PacketKey key = PacketKey.GetKey(data);
                    IPacketProcessor processor = __PacketProcessorStorage.GetProcessor(key);

                    IPacket packet = processor.Parser.Deserialize(data);
                    processor.Handler.Handle(packet);
                });

            __RunningHandles.Add(task);
        }

        public void Send(IPacket packet)
        {
            IPacketProcessor processor = __PacketProcessorStorage.GetProcessor(packet.Id);

            byte[] packetData = processor.Parser.Serialize(packet);
            __Exchanger.Send(packetData);
        }
        
        public void Start()
        {
            __Exchanger.DataRecieved += DataRecievedAsync;
            __Exchanger.Start();
        }

        public void Stop()
        {
            __Exchanger.Stop();
            __Exchanger.DataRecieved -= DataRecievedAsync;

            while (__RunningHandles.Count > 0)
                Thread.Sleep(10);
        }
    }
}
