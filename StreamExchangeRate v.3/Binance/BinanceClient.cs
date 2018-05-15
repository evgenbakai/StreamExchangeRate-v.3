using StreamExchangeRate_v._3.ModelTicker;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace StreamExchangeRate_v._3.Binance
{
    class BinanceClient : WebSocketProvider
    {
        protected override string ProviderName { get { return "Binance"; } }

        public BinanceClient(string providerId)
        {
            ProviderId = providerId;

            Uri uri = null;
            try
            {
                string streamName = string.Empty;

                List<string> symbol = _mappings[ProviderId].getListSymbol();
                Console.WriteLine($"{ProviderName} {symbol.Count} pairs: {string.Join(" ", symbol).ToUpper()} \n");

                _mappings[ProviderId].getListSymbolApi().ForEach(s => streamName += $"{s}@ticker/");
                uri = new Uri($"wss://stream.binance.com:9443/stream?streams={streamName.ToLower()}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error for ProviderId: {ProviderId} \nex.Message: {ex.Message}");
            }

            Init(uri, ProviderName);
        }

        protected override void OnMessage(string data)
        {
            BinanceStreamTick eventData = JsonConvert.DeserializeObject<BinanceStreamTick>(data);
            BaseTicker ticker = new BaseTicker();
            ticker.Symbol = _mappings[ProviderId].getSymbol(eventData.Data.SymbolApi);
            ticker.AskPrice = eventData.Data.BestAskPrice;
            ticker.BidPrice = eventData.Data.BestBidPrice;
            ticker.TotalTradedVolume = eventData.Data.TotalTradedQuoteAssetVolume;

            if (processTicker(ticker))
                Console.WriteLine($"[{ProviderName}] {ticker.Symbol} : Ask = {ticker.AskPrice}  Bid = {ticker.BidPrice} Volume = {ticker.TotalTradedVolume}");
            else
                Console.WriteLine($"[{ProviderName}] {ticker.Symbol}: data not changed");
        }
    }
}