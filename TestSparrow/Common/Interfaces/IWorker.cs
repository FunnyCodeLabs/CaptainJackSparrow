using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    interface IWorker
    {
        void Start();
        void Stop();
        void Run();
    }
}
