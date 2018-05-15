﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3.BitFlyer
{
    class BitFlyerStreamTick
    {
        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("tick_id")]
        public long TickId { get; set; }

        [JsonProperty("best_bid")]
        public decimal BestBid { get; set; }

        [JsonProperty("best_ask")]
        public decimal BestAsk { get; set; }

        [JsonProperty("best_bid_size")]
        public double BestBidSize { get; set; }

        [JsonProperty("best_ask_size")]
        public double BestAskSize { get; set; }

        [JsonProperty("total_bid_depth")]
        public double TotalBidDepth { get; set; }

        [JsonProperty("total_ask_depth")]
        public double TotalAskDepth { get; set; }

        [JsonProperty("ltp")]
        public double LatestPrice { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("volume_by_product")]
        public decimal VolumeByProduct { get; set; }

    }
}
