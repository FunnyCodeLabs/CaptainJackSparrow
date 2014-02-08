using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public class Connection : WorkerBase, IConnection
    {
        private IPacketExchanger __Exchanger;
        private IPacketHandlerStorage __PacketProcessorStorage;

        private List<Task> __RunningHandles = new List<Task>();

        public Connection(IPacketExchanger exchanger, IPacketHandlerStorage storage)
        {
            __Exchanger = exchanger;
            __PacketProcessorStorage = storage;
        }

        private void HandlePacketRecievedAsync(IPacket packet)
        {
            var task = Task.Factory.StartNew(() =>
                {
                    PacketKey key = packet.Id;
                    IPacketHandler handler = __PacketProcessorStorage.GetHandler(key);

                    handler.Handle(packet);
                });

            __RunningHandles.Add(task);
        }

        public void Send(IPacket packet)
        {
            __Exchanger.Send(packet);
        }
        
        public override void Start()
        {
            __Exchanger.DataRecieved += HandlePacketRecievedAsync;
            __Exchanger.Start();
        }

        public override void Stop()
        {
            __Exchanger.Stop();
            __Exchanger.DataRecieved -= HandlePacketRecievedAsync;

            while (__RunningHandles.Count > 0)
                Thread.Sleep(10);
        }
    }
}
