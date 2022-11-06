using Cryptocurrency.Application.Dto.ServiceResultDto;
using Cryptocurrency.Application.Services;

namespace Cryptocurrency.Application.Interfaces
{
    public interface ICryptocurrencyHandler
    {
        Task<ServiceResult<CryptoListDto>> GetCryptoSymbols();
        Task<ServiceResult<CryptoPriceDto>> GetCryptoPrices(string baseCrypto, List<string> currencies);
    }
}
