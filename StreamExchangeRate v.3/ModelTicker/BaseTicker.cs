using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3.ModelTicker
{
    public class BaseTicker
    {
        public string Symbol { get; set; }
        public decimal AskPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal TotalTradedVolume { get; set; }
    }
}
