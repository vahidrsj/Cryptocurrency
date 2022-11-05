using Refit;

namespace Cryptocurrency.Infrastructure.Services.API
{
    public interface IExchangeRateAPI
    {
        [Headers("apikey:rOgQLD9X4adEP2H9rSjnQoL0MYpcRl99")]
        [Get("/exchangerates_data/latest?base={baseCurrency}&symbols={symbols}")]
        Task<object> GetRates(string baseCurrency, string symbols);
    }
}
