using Coinbin.net.Util;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Coinbin.net.Test
{
    public class CoinbinAPITests : IDisposable
    {
        CoinbinAPI _coinbin;

        public CoinbinAPITests()
        {
            _coinbin = new CoinbinAPI();
        }

        public void Dispose()
        {
            _coinbin.Dispose();
        }

        [Fact]
        public async Task GetCoinDetailsReturnsCoinDetails()
        {
            var response = await _coinbin.GetCoinDetails("eth");

            Assert.Equal("Ethereum", response.Name);
        }

        [Fact]
        public async Task GetCoinDetailsWithWrongCoinThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinDetails("booga"));
        }

        [Fact]
        public async Task GetCoinDetailsWithEmptyCoinThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinDetails(""));
        }

        [Fact]
        public async Task GetCoinDetailsWithNullCoinThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinDetails(null));
        }

        [Fact]
        public async Task GetCoinValueReturnsCoinValue()
        {
            var response = await _coinbin.GetCoinValue("eth", 1.00m);

            // what else to assert??
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetCoinValueWithWrongCoinThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinValue("booga", 1.00m));
        }

        [Fact]
        public async Task GetCoinValueWithEmptyCoinThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinValue("", 1.00m));
        }

        [Fact]
        public async Task GetCoinValueWithNullCoinThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinValue(null, 1.00m));
        }

        [Fact]
        public async Task GetCoinValueWithNegativeInputThrows()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinValue("eth", -1.00m));
        }

        [Fact]
        public async Task GetCoinExchangeReturnsExchangeBetweenTwoCoins()
        {
            var response = await _coinbin.GetCoinExchange("eth", "btc");

            // what else to assert??
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetCoinExchangeThrowsIfInvalidFromCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchange("booga", "btc"));
        }

        [Fact]
        public async Task GetCoinExchangeThrowsIfInvalidToCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchange("eth", "booga"));
        }

        [Fact]
        public async Task GetCoinExchangeThrowsIfEmptyFromCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchange("", "btc"));
        }

        [Fact]
        public async Task GetCoinExchangeThrowsIfEmptyToCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchange("eth", ""));
        }

        [Fact]
        public async Task GetCoinExchangeThrowsIfNullFromCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchange(null, "btc"));
        }

        [Fact]
        public async Task GetCoinExchangeThrowsIfNullToCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchange("eth", null));
        }

        [Fact]
        public async Task GetCoinExchangeValReturnsValue()
        {
            var response = await _coinbin.GetCoinExchangeValue("eth", 2.00m, "btc");

            Assert.Equal("btc", response.ValueCoin);
        }

        [Fact]
        public async Task GetCoinExchangeValThrowsIfInvalidFromCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchangeValue("booga", 1.00m, "btc"));
        }

        [Fact]
        public async Task GetCoinExchangeValThrowsIfInvalidToCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchangeValue("eth", 1.00m, "booga"));
        }

        [Fact]
        public async Task GetCoinExchangeValThrowsIfEmptyFromCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchangeValue("", 1.00m, "btc"));
        }

        [Fact]
        public async Task GetCoinExchangeValThrowsIfEmptyToCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchangeValue("", 1.00m, "booga"));
        }

        [Fact]
        public async Task GetCoinExchangeValThrowsIfNullFromCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchangeValue(null, 1.00m, "btc"));
        }

        [Fact]
        public async Task GetCoinExchangeValThrowsIfNullToCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinExchangeValue("eth", 1.00m, null));
        }

        [Fact]
        public async Task GetCoinHistoryReturnsHistory()
        {
            var response = await _coinbin.GetCoinHistory("eth");

            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetCoinHistoryThrowsIfInvalidCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinHistory("booga"));
        }

        [Fact]
        public async Task GetCoinHistoryThrowsIfEmptyCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinHistory(""));
        }

        [Fact]
        public async Task GetCoinHistoryThrowsIfNullCoin()
        {
            await Assert.ThrowsAsync<CoinbinException>(async () =>
                await _coinbin.GetCoinHistory(null));
        }

        [Fact]
        public async Task GetCoinsReturnsCoins()
        {
            var response = await _coinbin.GetCoins();

            Assert.NotEmpty(response);
        }
    }
}
