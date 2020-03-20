using System;
using System.Threading;

namespace ProxyClass
{
    public class Runner : MarshalByRefObject
    {
        public string Name { get; private set; }

        public Runner(string name)
        {
            this.Name = name;
        }

        public void Run()
        {
            Console.WriteLine($"'{Name}' start run");
            Thread.Sleep(1000);
            Console.WriteLine($"'{Name}' stop run");
        }
    }
}
