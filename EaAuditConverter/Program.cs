using System;
using System.Threading;

namespace EaAuditConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = new AuditConverterConsole(args);

            bool ok = converter.Execute();

            Console.ReadKey();

        }
     }
}
