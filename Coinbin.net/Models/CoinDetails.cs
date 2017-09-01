using Newtonsoft.Json;

namespace Coinbin.net.Models
{
    public class CoinDetail
    {
        public decimal BTC { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public string Ticker { get; set; }
        public decimal USD { get; set; }
    }
}