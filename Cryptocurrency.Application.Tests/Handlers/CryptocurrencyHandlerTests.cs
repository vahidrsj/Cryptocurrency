using Cryptocurrency.Application.Constants;
using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Handlers;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

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
            var expectedResult = new ServiceResult<CoinMarketAPIDto>(new CoinMarketAPIDto()
            {
                Data = new List<CoinMarketAPIDataDto>() { new CoinMarketAPIDataDto() { name = cryptoName, symbol = cryptoSymbol } },
                Status = new CoinMarketAPIStatusDto()
            });

            cryptoListServiceMock.Setup(s => s.GetSymbols())
                                 .Returns(Task.FromResult(expectedResult));

            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);

            var result = await cryptoHandler.GetCryptoSymbols();

            Assert.IsNotNull(result);
            Assert.True(result.IsSuccessfull);
            Assert.IsNull(result.ErrorInfo);
            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.Data);
        }
    }
}
