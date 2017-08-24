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

        public async Task<CoinDetail> GetCoinDetails(Coin coin)
        {
            var url = BaseURL
                        .AppendPathSegment(coin.GetDescription())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinDetail>>(url);

            return content.Coin;
        }

        public async Task<CoinValue> GetCoinValue(Coin coin, decimal value)
        {
            var url = BaseURL
                        .AppendPathSegment(coin.GetDescription())
                        .AppendPathSegment(Math.Round(value, 2).ToString())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinValue>>(url);

            return content.Coin;
        }

        public async Task<CoinExchange> GetCoinExchange(Coin fromCoin, Coin toCoin)
        {
            var url = BaseURL
                        .AppendPathSegment(fromCoin.GetDescription())
                        .AppendPathSegment("to")
                        .AppendPathSegment(toCoin.ToString())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinExchange>>(url);

            return content.Coin;
        }

        public async Task<CoinExchangeValue> GetCoinExchangeValue (Coin fromCoin, decimal fromValue, Coin toCoin)
        {
            var url = BaseURL
                        .AppendPathSegment(fromCoin.GetDescription())
                        .AppendPathSegment(Math.Round(fromValue, 2).ToString())
                        .AppendPathSegment("to")
                        .AppendPathSegment(toCoin.ToString())
                        .Build();

            var content = await MakeRequestAsync<CoinWrapper<CoinExchangeValue>>(url);

            return content.Coin;
        }

        public async Task<List<CoinHistory>> GetCoinHistory(Coin coin)
        {
            var url = BaseURL
                        .AppendPathSegment(coin.GetDescription())
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

        public async Task<List<Coin>> GetCoins()
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

        public void Dispose()
            => _httpClient.Dispose();
    }
}
