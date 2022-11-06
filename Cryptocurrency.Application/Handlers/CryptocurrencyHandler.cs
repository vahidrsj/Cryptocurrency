using Cryptocurrency.Application.Constants;
using Cryptocurrency.Application.Dto.ServiceResultDto;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Mapping;
using Cryptocurrency.Application.Services;
using Cryptocurrency.Core.Entities;
using Cryptocurrency.Core.Enums;
using Cryptocurrency.Core.Errors;
using Cryptocurrency.Core.ValueObject;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Cryptocurrency.Application.Handlers
{
    public class CryptocurrencyHandler : ICryptocurrencyHandler
    {
        private readonly ICryptoListService coinMarketAPI;
        private readonly ICryptoPriceService exchangeRateAPI;
        private readonly ILogger<CryptocurrencyHandler> logger;
        private readonly IMemoryCache memoryCache;

        public CryptocurrencyHandler(ICryptoListService coinMarketAPI, ICryptoPriceService exchangeRateAPI, ILogger<CryptocurrencyHandler> logger, IMemoryCache memoryCache)
        {
            this.coinMarketAPI = coinMarketAPI ?? throw new ArgumentNullException(nameof(coinMarketAPI));
            this.exchangeRateAPI = exchangeRateAPI ?? throw new ArgumentNullException(nameof(exchangeRateAPI));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<ServiceResult<CryptoListDto>> GetCryptoSymbols()
        {
            logger.LogInformation("Getting symbols using Coinmarket api.");

            var casheResult = memoryCache.Get<CryptoListDto>(CacheKeys.CryptoListKey);
            if (casheResult == null)
            {
                var symbolResult = await FetchCryptoSymbols();
                if (!symbolResult.IsSuccessfull)
                {
                    logger.LogError(symbolResult.ErrorInfo.ToString());
                    return new ServiceResult<CryptoListDto>(symbolResult.ErrorInfo);
                }

                memoryCache.Set(CacheKeys.CryptoListKey, symbolResult.Result, TimeSpan.FromMinutes(10));
                logger.LogInformation("Crypto name list stored in cache. {@casheValues}", symbolResult.Result);

                return new ServiceResult<CryptoListDto>(symbolResult.Result);
            }
            else
            {
                logger.LogInformation("Crypto name list fetched from cache. {@casheResult}", casheResult);
                return new ServiceResult<CryptoListDto>(casheResult);
            }
        }

        public async Task<ServiceResult<CryptoPriceDto>> GetCryptoPrices(string baseCrypto, List<string> currencies)
        {
            if (string.IsNullOrWhiteSpace(baseCrypto))
            {
                var error = new ErrorResultDto() { ErrorType = ErrorTypes.InvalidParameter };
                logger.LogError(error.ToString());
                return new ServiceResult<CryptoPriceDto>(error);
            }

            var validateResult = await ValidateSymbol(baseCrypto);
            if (!validateResult.IsSuccessfull)
            {
                logger.LogError(validateResult.ErrorInfo.ToString());
                return new ServiceResult<CryptoPriceDto>(validateResult.ErrorInfo);
            }

            var crypto = new Crypto(validateResult.Result);

            logger.LogInformation($"Getting {baseCrypto} prices using Exchangerate api.");

            var response = await exchangeRateAPI.GetRates(baseCrypto.ToUpper(), currencies);
            if (!response.IsSuccessfull)
            {
                logger.LogError(response.ErrorInfo.ToString());
                return new ServiceResult<CryptoPriceDto>(response.ErrorInfo);
            }

            var resultStatus = response.Result.Success;
            if (resultStatus)
            {
                crypto.SetPrice(response.Result.Rates);

                var priceResult = crypto.ToCryptoPriceDto();

                logger.LogInformation("Crypto prices was successfully fetched. {@priceResult}", priceResult);

                return new ServiceResult<CryptoPriceDto>(priceResult);
            }
            else
            {
                var error = new ErrorResultDto() { ErrorType = ErrorTypes.APIReturnError };
                logger.LogError(error.ToString());
                return new ServiceResult<CryptoPriceDto>(error);
            }
        }

        private async Task<ServiceResult<CryptoName>> ValidateSymbol(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                var error = new ErrorResultDto() { ErrorType = ErrorTypes.InvalidParameter };
                logger.LogError(error.ToString());
                return new ServiceResult<CryptoName>(error);
            }

            var cryptoList = await GetCryptoSymbols();
            if (!cryptoList.IsSuccessfull)
            {
                logger.LogError(cryptoList.ErrorInfo.ToString());
                return new ServiceResult<CryptoName>(cryptoList.ErrorInfo);
            }

            var sumbolList = cryptoList.Result.Data
                                              .Where(c => c.symbol.ToUpper() == symbol.ToUpper());
            if (sumbolList.Any())
            {
                var specifiedSumbol = sumbolList.First();
                return new ServiceResult<CryptoName>(new CryptoName(specifiedSumbol.name, specifiedSumbol.symbol));
            }
            else
                return new ServiceResult<CryptoName>(new ErrorResultDto() { ErrorType = ErrorTypes.NotFound });

        }

        private async Task<ServiceResult<CryptoListDto>> FetchCryptoSymbols()
        {
            var response = await coinMarketAPI.GetSymbols();

            if (!response.IsSuccessfull)
            {
                logger.LogError(response.ErrorInfo.ToString());
                return new ServiceResult<CryptoListDto>(response.ErrorInfo);
            }

            var resultStatus = response.Result.Status;
            if (resultStatus.error_code == 0)
            {
                var symbolResult = response.Result.ToCryptoListDto();

                logger.LogInformation("Crypto name list was successfully fetched. {@symbolResult}", symbolResult);

                return new ServiceResult<CryptoListDto>(symbolResult);
            }
            else
            {
                var error = new ErrorResultDto() { ErrorType = ErrorTypes.APIReturnError, Message = resultStatus.error_message };
                logger.LogError(error.ToString());

                return new ServiceResult<CryptoListDto>(error);
            }
        }
    }
}
