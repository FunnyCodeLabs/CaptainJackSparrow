using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communication
{
    public class Connection : WorkerBase, IConnection
    {
        public const int CONNECTION_CHECK_FREQ = 1000;

        private IPacketExchanger __Exchanger;
        private IPacketHandlerStorage __PacketProcessorStorage;

        private List<Task> __RunningHandles = new List<Task>();
        private Task __ConnectionChecker;

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

        protected virtual void OnConnectionClosed()
        {
            ConnectionClosedEventHandler handler = ConnectionClosed;
            if (handler != null)
            {
                handler(this, this);
            }
        }

        public void Send(IPacket packet)
        {
            __Exchanger.Send(packet);
        }
        
        public override void Start()
        {
            __Exchanger.DataRecieved += HandlePacketRecievedAsync;
            __Exchanger.Start();

            __ConnectionChecker = Task.Factory.StartNew(() =>
                {
                    while (__Exchanger.Active)
                        Thread.Sleep(CONNECTION_CHECK_FREQ);

                    OnConnectionClosed();

                    if (!__Stopped)
                        Stop();
                });
        }

        public override void Stop()
        {
            __Exchanger.Stop();
            __Exchanger.DataRecieved -= HandlePacketRecievedAsync;

            while (__RunningHandles.Count > 0)
                Thread.Sleep(10);

            OnConnectionClosed();
        }

        public event ConnectionClosedEventHandler ConnectionClosed;

        public IPacketExchanger Exchanger
        {
            get { return __Exchanger; }
        }
    }
}
