using Cryptocurrency.Application.Services;

namespace Cryptocurrency.Application.Interfaces
{
    public interface ICryptocurrencyHandler
    {
        Task<ServiceResult<string>> GetCryptoSymbols();
    }
}
