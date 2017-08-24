using Newtonsoft.Json;

namespace Coinbin.net.Models
{
    public class CoinValue
    {
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }
        public decimal Value { get; set; }

        [JsonProperty("value.currency")]
        public string Currency { get; set; }
    }
}
