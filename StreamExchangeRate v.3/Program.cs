using StreamExchangeRate_v._3.Binance;
using StreamExchangeRate_v._3.BitFlyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Provider p0 = new BitFlyerClient("31");
            p0.Start();

            Provider p = new BinanceClient("24");
            p.Start();
            Console.ReadKey();
        }
    }
}
