# Cryptocurrency

> A simple console app to show the rates of a specified cryptocurrency in other currencies

- [Cryptocurrency](#truelayer-hachernews)
  - [Prerequisites](#prerequisites)
  - [Build with](#build-with)
  - [Settings](#settings)
  - [API](#apis)
  - [Docker](#docker)
## Prerequisites

For compiling and building you need:

- [Visual Studio 2022 (Latest version)](https://visualstudio.microsoft.com/) installed on your development machine.

## Build with
- [.Net 6](https://dotnet.microsoft.com/download/dotnet/6.0)

## Settings
- API URLs for list the crypocurrencies, find the latest prices, and supported currencies located in **appsettings.json** file.

## APIs
This app uses 2 APIs:
  - [CoinMarketCap API (Free version)](https://coinmarketcap.com/api/documentation/v1/#operation/getV1CryptocurrencyMap) for getting the Cryptocurrency symbols.
  - [Exchangerates API (Free version)](https://apilayer.com/marketplace/exchangerates_data-api) for getting the quotes of a specified cryptocurrency.

Actually, I wanted to develope an app that accepts any known cryptocurrency symbols as input and returns the rates in considered currencies. But there was some problems on using the APIs: 
- The **ExchangeRates symbols API** lists currency and cryptocurrency symbols together and there is no separation flag for these two types of currencies. For example, I could not know whether the DOP symbol is for currency or it is cryptocurrency. As well as, it only shows the **Bitcoin** in the list of symbols and any other cryptocurrency symbols are unknown (e.g. ETH, USDT).
- I tried to use the **CoinMarketCap Latest quotes API** for gathering the quotes in (USD, EUR, BRL, GBP, and AUD) currencies but in the free version of CoinMarketCap APIs requesting the quotes in more than one currency is not accessible. 

So, the specified user story only works for BTC (Bitcoin) cryptocurrency because of these limits.


## Docker

Target OS: Linux

Build: 
```
docker build -f Cryptocurrency.ConsoleUI\Dockerfile -t cryptocurrency .
```

Run:
```
docker run -it cryptocurrency:latest

```

