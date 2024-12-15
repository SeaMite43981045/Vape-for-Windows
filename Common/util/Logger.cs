using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vape_for_Windows.Common.util
{
    internal enum LogSource {
        [Description("Vape for Windows")]
        System,
        [Description("Module Manager")]
        Module,
    }

    internal class Logger
    {
        public static void Log(LogSource source, string message)
        {
            Console.WriteLine("INFO [" + source.ToString() + "] "+ message);
        }

        public static void Warn(LogSource source, string message)
        {
            Console.WriteLine("WARN [" + source.ToString() + "] "+ message);
        }

        public static void Error(LogSource source, string message)
        {
            Console.WriteLine("ERROR [" + source.ToString() + "] " + message);
        }
    }
}
