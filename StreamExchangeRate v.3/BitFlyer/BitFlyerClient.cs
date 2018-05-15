using Newtonsoft.Json;
using StreamExchangeRate_v._3.ModelTicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3.BitFlyer
{
    class BitFlyerClient : RestProvider
    {
        private string ProviderName { get; set; } = "BitFlyer";

        public BitFlyerClient(string providerId)
        {
            ProviderId = providerId;

            string[] url;
            try
            {
                string streamName = string.Empty;

                List<string> symbol = _mappings[ProviderId].getListSymbol();
                Console.WriteLine($"{ProviderName} {symbol.Count} pairs: {string.Join(" ", symbol).ToUpper()} \n");

                List<string> listSymbolApi = _mappings[ProviderId].getListSymbolApi();

                url = new string[listSymbolApi.Count];
                for (int i = 0; i < listSymbolApi.Count; i++)
                {
                    url[i] = $"https://api.bitflyer.jp/v1/ticker?product_code={listSymbolApi[i]}";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error for ProviderId: {ProviderId} \nex.Message: {ex.Message}");
            }

            Init(url, ProviderName);
        }

        protected override void OnMessage(string data)
        {
            BitFlyerStreamTick eventData = JsonConvert.DeserializeObject<BitFlyerStreamTick>(data);
            BaseTicker ticker = new BaseTicker();
            ticker.Symbol = _mappings[ProviderId].getSymbol(eventData.ProductCode);

            ticker.AskPrice = eventData.BestAsk;
            ticker.BidPrice = eventData.BestBid;
            ticker.TotalTradedVolume = eventData.Volume;

            // check the data has changed
            if (processTicker(ticker))
                Console.WriteLine($"[{ProviderName}] {ticker.Symbol} : Ask = {ticker.AskPrice}  Bid = {ticker.BidPrice} Volume = {ticker.TotalTradedVolume}");
            else
            {
                Console.WriteLine($"[{ProviderName}] {ticker.Symbol}: data not changed");
            }
        }
    }
}
