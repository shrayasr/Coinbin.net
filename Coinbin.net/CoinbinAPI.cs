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

        public async Task<CoinDetail> GetCoinDetails(string coin)
        {
            ThrowIfInvalidCoin(coin);

            var url = BaseURL
                        .AppendPathSegment(coin)
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinDetail>>(url);

            return content.Coin;
        }

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

        public async Task<CoinExchangeValue> GetCoinExchangeValue (string fromCoin, decimal fromValue, string toCoin)
        {
            var url = BaseURL
                        .AppendPathSegment(fromCoin)
                        .AppendPathSegment(Math.Round(fromValue, 2).ToString())
                        .AppendPathSegment("to")
                        .AppendPathSegment(toCoin.ToString())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinExchangeValue>>(url);

            return content.Coin;
        }

        public async Task<List<CoinHistory>> GetCoinHistory(string coin)
        {
            var url = BaseURL
                        .AppendPathSegment(coin)
                        .AppendPathSegment("history")
                        .Build();

            var content = await MakeRequestAsync<HistoryWrapper<List<CoinHistory>>>(url);

            return content.History;
        }

        /*
         * TODO implement this
         * 
         * The current problem here is the deserialization from 1ST -> FirstBlood and b@ -> BankCoin.
         * I want to retain these in a Enum because it expersses the problem the best.
         *
        public async Task<List<Coin>> GetCoins()
        {
            var url = BaseURL
                        .AppendPathSegment("coins")
                        .Build();

            var content = await MakeRequestAsync<CoinsWrapper<List<Coin>>>(url);

            return content.Coins;
        }
         */

        public async Task GetCoins()
            => throw new NotImplementedException("This API hasn't been implemented yet.");

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
            if (!CoinValidator.IsValid(coin))
                throw new CoinbinException($"{coin} isn't a valid coin");
        }

        public void Dispose()
            => _httpClient.Dispose();
    }
}
