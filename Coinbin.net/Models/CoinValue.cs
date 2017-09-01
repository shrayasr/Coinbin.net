using Newtonsoft.Json;

namespace Coinbin.net.Models
{
    public class CoinValue
    {
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }
        public decimal USD { get; set; }
    }
}
