using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Cryptocurrency.Core.Errors;
using Cryptocurrency.Infrastructure.Services.API;

namespace Cryptocurrency.Infrastructure.Services
{
    public class CryptoPriceService : ICryptoPriceService
    {
        private IExchangeRateAPI exchangeRateAPI;
        public CryptoPriceService(IExchangeRateAPI exchangeRateAPI)
        {
            this.exchangeRateAPI = exchangeRateAPI;
        }

        public async Task<ServiceResult<ExchangeRateAPIDto>> GetRates(string baseCurrency, List<string> currencies)
        {
            try
            {
                var rateResult = await exchangeRateAPI.GetRates(baseCurrency, string.Join(",", currencies));
                return new ServiceResult<ExchangeRateAPIDto>(rateResult);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ExchangeRateAPIDto>(new ErrorResultDto() { ErrorType = Core.Enums.ErrorTypes.APICallError, Message = ex.Message });
            }
        }
    }
}
