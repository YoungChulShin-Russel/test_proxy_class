using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProxyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner ycRunner = PerformanceCheckProxy<Runner>.Create(new Runner("신영철"));
            ycRunner.Run();

            Console.ReadLine();
        }
    }
}
