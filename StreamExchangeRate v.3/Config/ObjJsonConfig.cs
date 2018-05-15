using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StreamExchangeRate_v._3.Config
{
    public class ObjJsonConfig
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("providers")]
        public IList<Provider> Providers { get; set; }
    }
    public class Provider
    {
        [JsonProperty("providerId")]
        public string ProviderId { get; set; }

        [JsonProperty("mappedTo")]
        public string mappedTo { get; set; }
    }
}
