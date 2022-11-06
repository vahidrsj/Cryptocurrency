using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Services;

namespace Cryptocurrency.Application.Interfaces
{
    public interface ICryptoPriceService
    {
        Task<ServiceResult<ExchangeRateAPIDto>> GetRates(string baseCurrency);
    }
}
