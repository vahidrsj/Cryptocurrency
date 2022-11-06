using Cryptocurrency.Application.Dto.APIDto;
using Refit;

namespace Cryptocurrency.Infrastructure.Services.API
{
    public interface ICoinMarketAPI
    {
        [Headers("X-CMC_PRO_API_KEY:f69d461b-9b58-4797-b72a-7752a48594d6")]
        [Get("/v1/cryptocurrency/map?sort=cmc_rank&limit=30")]
        Task<CoinMarketAPIDto> GetSymbols();
    }
}
