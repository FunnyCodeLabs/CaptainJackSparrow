using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class InformationManager : IInformationManager
    {
        public void ShowInfo(string str, bool showDateTime = true)
        {
            if (showDateTime)
                Console.WriteLine(String.Format("[{0}] {1}", DateTime.Now.ToString("DD/MM/YYYY - HH:mm:ss"), str));
            else
                Console.WriteLine(String.Format("{0}", str));
        }

        public void ShowInfo(IPacket packet)
        {
            ShowInfo(packet.ToString());
        }
    }
}
