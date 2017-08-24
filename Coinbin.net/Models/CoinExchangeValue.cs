using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Coinbin.net.Models
{
    public class CoinExchangeValue
    {
        public decimal Value { get; set; }

        [JsonProperty("value.coin")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Coin ValueCoin { get; set; }

        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }
    }
}
