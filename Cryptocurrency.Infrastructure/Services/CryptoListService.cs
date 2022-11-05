using Cryptocurrency.Application.Interfaces;
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

        public async Task<object> GetMaps()
        {
            var mapResult = await coinMarketAPI.GetMaps();

            return mapResult;

            //var request = new RestRequest("v1/cryptocurrency/map", Method.Get);
            //request.Timeout = -1;
            //request.AddHeader("X-CMC_PRO_API_KEY", apiKey);
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
