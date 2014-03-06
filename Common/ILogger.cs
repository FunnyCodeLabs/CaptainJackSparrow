using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ILogger
    {
        void Write(String str);
        void WriteCritical(String str);
        void WriteError(String str);
    }
}
