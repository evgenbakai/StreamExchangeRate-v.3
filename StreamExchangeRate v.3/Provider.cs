using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamExchangeRate_v._3.ModelTicker;
using StreamExchangeRate_v._3.Config;
using System.Collections.Concurrent;

namespace StreamExchangeRate_v._3
{
    public abstract class Provider : IDisposable
    {
        private TotalTicker totalTicker;

        public Provider(string fileConfigJson = "mappings.json")
        {
            try
            {
                totalTicker = TotalTicker.Instance();
                var mappedSymbols = ConfigurationManager.GetJson(fileConfigJson);
                initializeMapping(mappedSymbols);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        protected static ConcurrentDictionary<string, ProviderConfig> _mappings = new ConcurrentDictionary<string, ProviderConfig>();

        protected string ProviderId { get; set; }

        protected abstract void OnMessage(string data);
        public abstract Task Start();
        public abstract void Stop();
        public abstract void Dispose();

        private static void initializeMapping(List<ObjJsonConfig> mappedSymbols)
        {
            foreach (var mappedSymbol in mappedSymbols)
            {
                foreach (var provider in mappedSymbol.Providers)
                {
                    if (!_mappings.ContainsKey(provider.ProviderId))
                        _mappings[provider.ProviderId] = new ProviderConfig();

                    var symbolConfig = new SymbolConfig()
                    {
                        Symbol = mappedSymbol.Symbol,
                        ProviderId = provider.ProviderId,
                        SymbolApi = provider.mappedTo
                    };
                    _mappings[provider.ProviderId].addSymbolConfig(symbolConfig);
                }
            }
        }

        protected bool processTicker(BaseTicker ticker)
        {
            // true - тікер обробили, його потрібно вивести
            // false - тікер не змінився
            bool processed = false;
            if (ticker.Symbol != string.Empty && ticker.BidPrice != 0 && ticker.AskPrice != 0 && ticker.TotalTradedVolume != 0)
            {
                if (totalTicker.dictionaryTicker.ContainsKey(ticker.Symbol))
                {
                    BaseTicker updatedTicker;
                    processed = calculateTicker(ticker, out updatedTicker);
                    if (processed)
                    {
                        //totalTicker.dictionaryTicker[ticker.Symbol] = updatedTicker;
                        totalTicker.dictionaryTicker.AddOrUpdate(ticker.Symbol, updatedTicker, (k, v) => updatedTicker);
                    }
                }
                else
                {
                    processed = true;
                    //totalTicker.dictionaryTicker[ticker.Symbol] = ticker;
                    totalTicker.dictionaryTicker.TryAdd(ticker.Symbol, ticker);
                }
            }
            return processed;
        }

        protected bool calculateTicker(BaseTicker newTicker, out BaseTicker resultTicker)
        {
            // true - прийшов новий тікер
            // false - тікер рівний тікеру в dictionaryTicker
            bool changed = false;
            BaseTicker lastTicker = totalTicker.dictionaryTicker[newTicker.Symbol];
            if (lastTicker.AskPrice != newTicker.AskPrice ||
                lastTicker.BidPrice != newTicker.BidPrice ||
                lastTicker.TotalTradedVolume != newTicker.TotalTradedVolume)
            {
                resultTicker = newTicker;
                changed = true;
            }
            else resultTicker = new BaseTicker();

            return changed;
        }

        
    }
}
