using ResdisPubSub.PubSub;
using System;
using System.Threading;

namespace ResdisPubSub
{
    class Program
    {
        static ISubscribeService client = new RedisSubscribe();
        static void Main(string[] args)
        {
            client.Subscribe("bigbigChannel", m => { Console.WriteLine($"我是bigbigChannel，接收到信息：{m}"); });
            Thread t = new Thread(Run);
            t.Start();
        }

        static void Run()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                client.Publish("bigbigChannel", i.ToString());
            }
       
        }
    }
}
