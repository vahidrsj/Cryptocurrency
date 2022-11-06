using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Dto.ServiceResultDto;
using Cryptocurrency.Core.Entities;
using Mapster;

namespace Cryptocurrency.Application.Mapping
{
    public static class Mapper
    {
        public static CryptoListDto APItoUI(CoinMarketAPIDto coinMarket)
        {
            return coinMarket.Adapt<CryptoListDto>();
        }

        public static CryptoPriceDto CryptoToUI(Crypto crypto)
        {
            return crypto.Adapt<CryptoPriceDto>();
        }
    }
}
