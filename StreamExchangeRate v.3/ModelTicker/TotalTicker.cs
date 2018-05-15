using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3.ModelTicker
{
    class TotalTicker
    {
        public ConcurrentDictionary<string, BaseTicker> dictionaryTicker = new ConcurrentDictionary<string, BaseTicker>();

        static TotalTicker uniqueInstance;

        public static TotalTicker Instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new TotalTicker();

            return uniqueInstance;
        }
        protected TotalTicker()
        {
        }
    }
}