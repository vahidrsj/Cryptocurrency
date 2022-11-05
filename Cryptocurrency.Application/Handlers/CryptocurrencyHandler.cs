using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Application.Handlers
{
    public class CryptocurrencyHandler: ICryptocurrencyHandler
    {
        private readonly ICryptoListService coinMarketAPI;
        private readonly ICryptoRateService exchangeRateAPI;
        private readonly ILogger<CryptocurrencyHandler> logger;

        public CryptocurrencyHandler(ICryptoListService coinMarketAPI, ICryptoRateService exchangeRateAPI, ILogger<CryptocurrencyHandler> logger)
        {
            this.coinMarketAPI = coinMarketAPI ?? throw new ArgumentNullException(nameof(coinMarketAPI));
            this.exchangeRateAPI = exchangeRateAPI ?? throw new ArgumentNullException(nameof(exchangeRateAPI));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceResult<string>> GetCryptoSymbols()
        {
            logger.LogInformation("Preparing API to get symbols");

            var response = await coinMarketAPI.GetMaps();

            return new ServiceResult<string>(response.ToString());
        }
    }
}
