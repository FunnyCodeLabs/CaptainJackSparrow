using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Logger: ILogger
    {
        public static string LOG_PATH = "Server_Log.txt";

        private StreamWriter __LogWriter;

        public static Logger __Logger = new Logger(LOG_PATH);
        public static Logger FileLogger { get { return __Logger; } }

        private Logger(string logPath)
        {
            __LogWriter = new StreamWriter(new FileStream(logPath, FileMode.Append));
        }

        ~Logger()
        {
            __LogWriter.Dispose();
        }

        private string GetDateTimeString()
        {
            return String.Format("[{0:HH:mm:ss.ff} - {0:dd/MM/yyyy}] ", DateTime.Now);
        }

        public void Write(string str)
        {
            __LogWriter.WriteLine(GetDateTimeString() + str);
        }

        public void WriteCritical(string str)
        {
            __LogWriter.WriteLine(GetDateTimeString() + "CRITICAL: " + str);
        }

        public void WriteError(string str)
        {
            __LogWriter.WriteLine(GetDateTimeString() + "Error: " + str);
        }
    }
}
