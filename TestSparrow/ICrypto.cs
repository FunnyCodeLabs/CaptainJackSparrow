using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSparrow
{
    public interface ICrypto
    {
        string Encode(string s);
        string Decode(string s);
    }
}
