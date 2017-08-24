using Newtonsoft.Json;

namespace Coinbin.net.Models
{
    public class CoinDetail
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Ticker { get; set; }
        public decimal Value { get; set; }
        
        [JsonProperty("value.currency")]
        public string Currency { get; set; }
    }
}