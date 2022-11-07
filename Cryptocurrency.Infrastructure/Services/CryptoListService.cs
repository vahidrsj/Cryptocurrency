using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Application.Services;
using Cryptocurrency.Core.Errors;
using Cryptocurrency.Infrastructure.Services.API;

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
                return new ServiceResult<CoinMarketAPIDto>(symbolResult);
            }
            catch (Exception ex)
            {
                return new ServiceResult<CoinMarketAPIDto>(new ErrorResultDto() { ErrorType = Core.Enums.ErrorTypes.APICallError, Message = ex.Message });
            }
        }
    }
}
