using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public abstract class WorkerBase: IWorker
    {
        protected Task __MainThread;

        protected bool __Stopped = false;

        protected virtual void JobProc()
        { }

        public virtual void Start()
        {
            __Stopped = false;
            __MainThread = Task.Factory.StartNew(JobProc);
        }

        public virtual void Stop()
        {
            __Stopped = true;
        }

        public virtual void Run()
        {
            __Stopped = false;
            JobProc();
        }
    }
}
