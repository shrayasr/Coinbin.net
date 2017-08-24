using Newtonsoft.Json;

namespace Coinbin.net.Models
{
    public class CoinExchange
    {
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }
    }
}
