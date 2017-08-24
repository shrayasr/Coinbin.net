using Newtonsoft.Json;
using System;

namespace Coinbin.net.Models
{
    public class CoinHistory
    {
        public DateTimeOffset Timestamp { get; set; }
        public decimal Value { get; set; }
        public string When { get; set; }

        [JsonProperty("value.currency")]
        public string ValueCurrency { get; set; }
    }
}
