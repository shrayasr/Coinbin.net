# Coinbin.net

A fully featured C# wrapper for the [coinbin.org](https://coinbin.org) API.

## Installation

```
PM> Install-Package coinbin.net
```
**NOTE: DOESN'T WORK YET**  

## Supported Platforms

This is written atop [.Net
Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
[1.5](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.5.md)
so it supports:

- .NET Core 1.0
- .NET Framework 4.6.1
- Mono 4.6
- Xamarin.iOS 10.0
- Xamarin.Android 7.0

## Usage

To start, create an instance of the `CoinbinAPI` class. It implements
`IDisposable` so using it within a `using` block is recommended. This
internally creates and uses **one** instance of `HttpClient` per instance of
`CoinbinAPI`.

```cs
using (var cb = new CoinbinAPI())
{
  // ...
}
```

The `cb` object can now be used to make calls to the Coinbin API

## Endpoints

The set of all coins are stored in the `Coin` enumeration. As coinbin is
updated I will keep this enumeration updated.

### Get a coin's details

```cs
cb.GetCoinDetails(Coin.ETH);
```

Returns the details of a given coin:

```
|----------+----------|
| Name     | Ethereum |
| Rank     | 2        |
| Ticker   | ETH      |
| Value    | 323.511  |
| Currency | USD      |
|----------+----------|
```

### Get a coin's value

```cs
cb.GetCoinValue(Coin.ETH, 42.01m);
```

Returns the value of the given coin in USD along with the exchange rate:

```
|--------------+--------------------|
| ExchangeRate | 326.315            |
| Value        | 13708.493149999998 |
| Currency     | USD                |
|--------------+--------------------|
```

### Get the exchange rate between 2 coins

```cs
cb.GetCoinExchange(Coin.ETH, Coin.LTC);
```

Returns the exchange rate between 2 coins:

```
| ExchangeRate | 6.489775873199441 |
```

### Get the exchange rate between 2 coins for a value

```cs
cb.GetCoinExchangeValue(Coin.ETH, 2.00m, Coin.LTC);
```

Returns the exchange rate between ETH and LTC for 2 coins of ETH

```
|--------------+--------------------|
| Value        | 12.979551746398881 |
| ValueCoin    | LTC                |
| ExchangeRate | 6.489775873199441  |
|--------------+--------------------|
```

### Get historical information for a coin

```cs
cb.GetCoinHistory(Coin.ETH);
```

Returns the historical information for ETH

```
|-----------------------------+--------+-----------+---------------|
| Timestamp                   | Value  | When      | ValueCurrency |
|-----------------------------+--------+-----------+---------------|
| 25-Aug-17 4:00:37 AM +00:00 | 325.61 | today     | USD           |
| 24-Aug-17 4:00:49 AM +00:00 | 317.52 | yesterday | USD           |
| 23-Aug-17 4:01:07 AM +00:00 | 314.79 | Aug 23    | USD           |
| ...                         | ...    | ...       | ...           |
|-----------------------------+--------+-----------+---------------|
```

This returns up to 4 years of daily USD data for the given coin

## Caveat

Since we use an `Enum` to represent the set of all coins, we can't use
identifiers for some coins:

- `1ST` for `FirstBlood`
- `B@` for `BankCoin`

So these are converted from `FirstBlood` back to `1ST` when sent to Coinbin.

## Pending

The `GetCoins` API currently throws a `NotImplementedException` and needs to be
implemented.

If you want to help out, check out the method in the code and send a PR :+1:
