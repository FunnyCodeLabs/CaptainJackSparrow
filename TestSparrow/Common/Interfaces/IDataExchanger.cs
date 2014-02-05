using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public delegate void DataRecievedEventHandler(byte[] data);

    interface IDataExchanger: IWorker
    {
        void Send(byte[] data);
        event DataRecievedEventHandler DataRecieved;
    }
}
