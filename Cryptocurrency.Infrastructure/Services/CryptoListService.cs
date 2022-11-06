using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Cryptocurrency.Infrastructure.Services.API;
using Refit;

namespace Cryptocurrency.Infrastructure.Services
{
    public class CryptoListService : ICryptoListService
    {
        private readonly ICoinMarketAPI coinMarketAPI;
        public CryptoListService(ICoinMarketAPI coinMarketAPI)
        {
            this.coinMarketAPI = coinMarketAPI;
        }

        public async Task<ServiceResult<CoinMarketAPIDto>> GetSymbols()
        {
            try
            {
                var symbolResult = await coinMarketAPI.GetSymbols();

                if (symbolResult != null)
                    return new ServiceResult<CoinMarketAPIDto>(symbolResult);
                else
                    return new ServiceResult<CoinMarketAPIDto>("An error occurred on API calling");
            }
            catch (Exception ex)
            {
                return new ServiceResult<CoinMarketAPIDto>(ex.Message);
            }
        }
    }
}
