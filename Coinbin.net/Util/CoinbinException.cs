using System;

namespace Coinbin.net.Util
{
    public class CoinbinException : Exception
    {
        public CoinbinException(string message, Exception ex)
            : base (message, ex) {  }

        public CoinbinException(string message)
            : base(message) { }

        public CoinbinException()
            : base() { }
    }
}
