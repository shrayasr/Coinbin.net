namespace Coinbin.net.Models.Wrappers
{
    public class CoinWrapper<T> where T : class
    {
        public T Coin { get; set; }
    }
}
