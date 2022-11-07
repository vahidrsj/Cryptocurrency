using Cryptocurrency.Application.Constants;
using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Dto.ServiceResultDto;
using Cryptocurrency.Application.Handlers;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Cryptocurrency.Core.Enums;
using Cryptocurrency.Core.Errors;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Constraints;

namespace Cryptocurrency.Application.Tests.Handlers
{
    public class CryptocurrencyHandlerTests
    {
        private CryptocurrencyHandler cryptoHandler;
        private Mock<ICryptoListService> cryptoListServiceMock;
        private Mock<ICryptoPriceService> cryptoPriceServiceMock;
        private Mock<ILogger<CryptocurrencyHandler>> loggerMock;
        private Mock<IMemoryCache> memoryCacheMock;

        private readonly string cryptoName = "Bitcoin";
        private readonly string cryptoSymbol = "BTC";

        [SetUp]
        public void Setup()
        {
            cryptoListServiceMock = new Mock<ICryptoListService>();
            cryptoPriceServiceMock = new Mock<ICryptoPriceService>();
            loggerMock = new Mock<ILogger<CryptocurrencyHandler>>();
            memoryCacheMock = new Mock<IMemoryCache>();

            cryptoHandler = new CryptocurrencyHandler(cryptoListServiceMock.Object,
                                                    cryptoPriceServiceMock.Object,
                                                    loggerMock.Object,
                                                    memoryCacheMock.Object);
        }

        [Test]
        public async Task GetCryptoSymbols_Done_Successfully()
        {
            var expectedResultMock = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() {
                    new CoinMarketAPIDataDto() {
                        name = cryptoName,
                        symbol = cryptoSymbol
                    }
                },
                Status = new CoinMarketAPIStatusDto()
            });

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                                 .ReturnsAsync(expectedResultMock);

            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);

            var result = await cryptoHandler.GetCryptoSymbols();

            Assert.IsNotNull(result);
            Assert.True(result.IsSuccessfull);
            Assert.IsNull(result.ErrorInfo);
            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.Data);
        }

        [Test]
        public async Task GetCryptoSymbols_Fetch_API_With_CallError()
        {
            var expectedResultMock = new ServiceResult<CoinMarketAPIDto>(new ErrorResultDto() { ErrorType = ErrorTypes.APICallError });

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                                 .ReturnsAsync(expectedResultMock);

            var result = await cryptoHandler.GetCryptoSymbols();

            Assert.IsNotNull(result);
            Assert.False(result.IsSuccessfull);
            Assert.IsNotNull(result.ErrorInfo);
            Assert.IsTrue(result.ErrorInfo.ErrorType == ErrorTypes.APICallError);
            Assert.IsNull(result.Result);
        }

        [Test]
        public async Task GetCryptoSymbols_Fetch_API_Successful_With_Error()
        {
            var expectedResultMock = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() { new CoinMarketAPIDataDto() },
                Status = new CoinMarketAPIStatusDto() { error_code = 1 }
            });

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                                 .ReturnsAsync(expectedResultMock);

            var result = await cryptoHandler.GetCryptoSymbols();

            Assert.IsNotNull(result);
            Assert.False(result.IsSuccessfull);
            Assert.IsNotNull(result.ErrorInfo);
            Assert.IsTrue(result.ErrorInfo.ErrorType == ErrorTypes.APIReturnError);
            Assert.IsNull(result.Result);
        }

        [Test]
        public async Task GetCryptoPrices_Done_Successfully()
        {
            var expectedResultMock = new ServiceResult<ExchangeRateAPIDto>(new ExchangeRateAPIDto()
            {
                Base = cryptoSymbol,
                Rates = new Dictionary<string, decimal>() { { "USD", 123.456M } },
                Success = true
            });

            var expectedResultSymbols = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() { new CoinMarketAPIDataDto() { name = cryptoName, symbol = cryptoSymbol } },
                Status = new CoinMarketAPIStatusDto()
            });

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                                 .ReturnsAsync(expectedResultSymbols);

            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);

            var currencyList = new List<string>() { "USD" };
            cryptoPriceServiceMock.Setup(s => s.GetRates(cryptoSymbol, currencyList))
                                  .ReturnsAsync(expectedResultMock);

            var result = await cryptoHandler.GetCryptoPrices(cryptoSymbol, currencyList);

            Assert.IsNotNull(result);
            Assert.True(result.IsSuccessfull);
            Assert.IsNull(result.ErrorInfo);
            Assert.IsNotNull(result.Result);
            Assert.That(result.Result.Symbol.Equals(cryptoSymbol));
            Assert.That(result.Result.Name.Equals(cryptoName));
            Assert.IsNotEmpty(result.Result.Prices);
        }

        [Test]
        public async Task GetCryptoPrices_Invalid_Parameter()
        {
            var result = await cryptoHandler.GetCryptoPrices(string.Empty, new List<string>());

            Assert.IsNotNull(result);
            Assert.IsNull(result.Result);
            Assert.False(result.IsSuccessfull);
            Assert.That(result.ErrorInfo.ErrorType == ErrorTypes.InvalidParameter);
        }

        [Test]
        public async Task GetCryptoPrices_Invalid_Symbol()
        {
            var expectedResultSymbols = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() { new CoinMarketAPIDataDto() { name = cryptoName, symbol = cryptoSymbol } },
                Status = new CoinMarketAPIStatusDto()
            });

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                     .ReturnsAsync(expectedResultSymbols);

            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);

            var currencyList = new List<string>() { "USD" };

            var result = await cryptoHandler.GetCryptoPrices("ETH", currencyList);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Result);
            Assert.False(result.IsSuccessfull);
            Assert.That(result.ErrorInfo.ErrorType == ErrorTypes.NotFound);
        }

        [Test]
        public async Task GetCryptoPrices_APICall_Error()
        {
            var expectedResultSymbols = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() { new CoinMarketAPIDataDto() { name = cryptoName, symbol = cryptoSymbol } },
                Status = new CoinMarketAPIStatusDto()
            });

            var expectedResult = new ServiceResult<ExchangeRateAPIDto>(new ErrorResultDto() { ErrorType = ErrorTypes.APICallError });

            var currencyList = new List<string>() { "USD" };

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                     .ReturnsAsync(expectedResultSymbols);

            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);


            cryptoPriceServiceMock.Setup(s => s.GetRates(cryptoSymbol, currencyList))
                                    .ReturnsAsync(expectedResult);

            var result = await cryptoHandler.GetCryptoPrices(cryptoSymbol, currencyList);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Result);
            Assert.False(result.IsSuccessfull);
            Assert.That(result.ErrorInfo.ErrorType == ErrorTypes.APICallError);
        }

        [Test]
        public async Task GetCryptoPrices_API_Return_Error()
        {
            var expectedResultSymbols = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() { new CoinMarketAPIDataDto() { name = cryptoName, symbol = cryptoSymbol } },
                Status = new CoinMarketAPIStatusDto()
            });

            var expectedResultMock = new ServiceResult<ExchangeRateAPIDto>(new ExchangeRateAPIDto()
            {
                Base = string.Empty,
                Rates = new Dictionary<string, decimal>(),
                Success = false
            });

            var currencyList = new List<string>() { "USD" };

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                     .ReturnsAsync(expectedResultSymbols);

            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);


            cryptoPriceServiceMock.Setup(s => s.GetRates(cryptoSymbol, currencyList))
                                    .ReturnsAsync(expectedResultMock);

            var result = await cryptoHandler.GetCryptoPrices(cryptoSymbol, currencyList);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Result);
            Assert.False(result.IsSuccessfull);
            Assert.That(result.ErrorInfo.ErrorType == ErrorTypes.APIReturnError);
        }

    }
}
