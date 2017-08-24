# Coinbin.net

A fully featured C# wrapper for the [coinbin.org](https://coinbin.org) API.

## Installation

```
PM> Install-Package coinbin.net
```

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
