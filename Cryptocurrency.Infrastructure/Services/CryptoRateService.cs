using Cryptocurrency.Application.Interfaces;
using Cryptocurrency.Infrastructure.Services.API;

namespace Cryptocurrency.Infrastructure.Services
{
    public class CryptoRateService : ICryptoRateService
    {
        private IExchangeRateAPI exchangeRateAPI;
        public CryptoRateService(IExchangeRateAPI exchangeRateAPI)
        {
            this.exchangeRateAPI = exchangeRateAPI;
        }

        public async Task<object> GetRates(string baseCurrency, string symbols)
        {
            var rateResult = await exchangeRateAPI.GetRates(baseCurrency, symbols);

            return rateResult;

            //var request = new RestRequest(url, Method.Get);
            //request.Timeout = -1;
            //request.AddHeader("apikey", apiKey);
            //try
            //{
            //    var response = await restClient.ExecuteGetAsync(request);
            //    return response;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}
