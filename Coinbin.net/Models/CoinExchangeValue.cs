using Newtonsoft.Json;

namespace Coinbin.net.Models
{
    public class CoinExchangeValue
    {
        public decimal Value { get; set; }

        [JsonProperty("value.coin")]
        public string ValueCoin { get; set; }

        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }
    }
}
