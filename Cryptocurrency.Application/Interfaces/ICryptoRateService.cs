namespace Cryptocurrency.Application.Interfaces
{
    public interface ICryptoRateService
    {
        Task<object> GetRates(string baseCurrency, string symbols);
    }
}
