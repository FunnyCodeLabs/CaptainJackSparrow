﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow.Common
{
    public interface IWorker
    {
        void Start();
        void Stop();
        void Run();
    }
}