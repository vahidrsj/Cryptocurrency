using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
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

        public async Task<ServiceResult<ExchangeRateAPIDto>> GetRates(string baseCurrency)
        {
            try
            {
                var rateResult = await exchangeRateAPI.GetRates(baseCurrency, "AUD,BRL,EUR,GBP,USD");

                if (rateResult != null)
                    return new ServiceResult<ExchangeRateAPIDto>(rateResult);
                else
                    return new ServiceResult<ExchangeRateAPIDto>("An error occurred on API calling");
            }
            catch (Exception ex)
            {
                return new ServiceResult<ExchangeRateAPIDto>(ex.Message);
            }
        }
    }
}
