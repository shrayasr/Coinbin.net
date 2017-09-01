using Coinbin.net.Models;
using Coinbin.net.Models.Wrappers;
using Coinbin.net.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coinbin.net
{
    public class CoinbinAPI : IDisposable
    {
        private HttpClient _httpClient;

        private UrlBuilder BaseURL
        {
            get
            {
                return new UrlBuilder()
                                .SetSchema("https")
                                .SetHost("coinbin.org");
            }
        }

        public CoinbinAPI()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Get the details of the given coin
        /// </summary>
        /// <param name="coin">Name of the coin</param>
        /// <returns>Details of the coin</returns>
        /// <exception cref="CoinbinException">Thrown when <paramref name="coin"/> is invalid</exception>
        public async Task<CoinDetail> GetCoinDetails(string coin)
        {
            ThrowIfInvalidCoin(coin);

            var url = BaseURL
                        .AppendPathSegment(coin)
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinDetail>>(url);

            return content.Coin;
        }

        /// <summary>
        /// Get the value of the coin in USD
        /// </summary>
        /// <param name="coin">Name of the coin</param>
        /// <param name="value">Value of the coin to convert</param>
        /// <returns>The exchange rate and the USD value of the coin</returns>
        /// <exception cref="CoinbinException">Thrown when <paramref name="coin"/> is invalid</exception>
        public async Task<CoinValue> GetCoinValue(string coin, decimal value)
        {
            ThrowIfInvalidCoin(coin);

            var url = BaseURL
                        .AppendPathSegment(coin)
                        .AppendPathSegment(Math.Round(value, 2).ToString())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinValue>>(url);

            return content.Coin;
        }

        /// <summary>
        /// Get the exchange rate between one coin to another
        /// </summary>
        /// <param name="fromCoin">Name of the source coin</param>
        /// <param name="toCoin">Name of the destination coin</param>
        /// <returns>The exchange rate between the two coins</returns>
        /// <exception cref="CoinbinException">Thrown when <paramref name="fromCoin"/> or <paramref name="toCoin"/> is invalid</exception>
        public async Task<CoinExchange> GetCoinExchange(string fromCoin, string toCoin)
        {
            ThrowIfInvalidCoin(fromCoin);
            ThrowIfInvalidCoin(toCoin);

            var url = BaseURL
                        .AppendPathSegment(fromCoin)
                        .AppendPathSegment("to")
                        .AppendPathSegment(toCoin)
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinExchange>>(url);

            return content.Coin;
        }

        /// <summary>
        /// Return the exchange rate and the value of one coin in terms of another
        /// </summary>
        /// <param name="fromCoin">Name of the source coin</param>
        /// <param name="fromValue">Value in source coin currency</param>
        /// <param name="toCoin">Name of the destination coin</param>
        /// <returns>The exchange rate and value in the destination coin currency</returns>
        /// <exception cref="CoinbinException">Thrown when <paramref name="fromCoin"/> or <paramref name="toCoin"/> is invalid</exception>
        public async Task<CoinExchangeValue> GetCoinExchangeValue(string fromCoin, decimal fromValue, string toCoin)
        {
            ThrowIfInvalidCoin(fromCoin);
            ThrowIfInvalidCoin(toCoin);

            var url = BaseURL
                        .AppendPathSegment(fromCoin)
                        .AppendPathSegment(Math.Round(fromValue, 2).ToString())
                        .AppendPathSegment("to")
                        .AppendPathSegment(toCoin.ToString())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinExchangeValue>>(url);

            return content.Coin;
        }

        /// <summary>
        /// Get up to four years of daily USD data for a given coin
        /// </summary>
        /// <param name="coin">Name of the coin</param>
        /// <returns>A list of daily values</returns>
        /// <exception cref="CoinbinException">Thrown when <paramref name="coin"/> is invalid</exception>
        public async Task<List<CoinHistory>> GetCoinHistory(string coin)
        {
            ThrowIfInvalidCoin(coin);

            var url = BaseURL
                        .AppendPathSegment(coin)
                        .AppendPathSegment("history")
                        .Build();

            var content = await MakeRequestAsync<HistoryWrapper<List<CoinHistory>>>(url);

            return content.History;
        }

        /// <summary>
        /// Get the list of all the coins
        /// </summary>
        /// <returns>A dictionary containing all the coins that can be queried for in the system</returns>
        public async Task<IDictionary<string, CoinDetail>> GetCoins()
        {
            var url = BaseURL
                        .AppendPathSegment("coins")
                        .Build();

            var content = await MakeRequestAsync<CoinsWrapper<IDictionary<string, CoinDetail>>>(url);

            return content.Coins;
        }

        private async Task<T> MakeRequestAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                throw new CoinbinException(ex.Message, ex);
            }
        }

        private void ThrowIfInvalidCoin(string coin)
        {
            if (coin.IsEmpty() || !CoinValidator.IsValid(coin))
                throw new CoinbinException($"{coin} isn't a valid coin");
        }

        public void Dispose()
            => _httpClient.Dispose();
    }
}
