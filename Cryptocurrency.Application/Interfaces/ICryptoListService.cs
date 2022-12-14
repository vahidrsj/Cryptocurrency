using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Services;

namespace Cryptocurrency.Application.Interfaces
{
    public interface ICryptoListService
    {
        Task<ServiceResult<CoinMarketAPIDto>> GetSymbols();
    }
}
