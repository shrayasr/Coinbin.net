namespace Coinbin.net.Models.Wrappers
{
    public class CoinsWrapper<T> where T : class
    {
        public T Coins { get; set; }
    }
}
