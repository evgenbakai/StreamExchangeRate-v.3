using StreamExchangeRate_v._3.Binance;
using StreamExchangeRate_v._3.BitFlyer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Provider> provider_list = new List<Provider>()
            {
                new BitFlyerClient("31"),
                new BinanceClient("24")
            };

            foreach (var p in provider_list)
                p.Start();

            Console.ReadKey();
        }
    }
}
