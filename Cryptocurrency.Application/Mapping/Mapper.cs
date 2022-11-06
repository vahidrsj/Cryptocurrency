using Cryptocurrency.Application.Dto.APIDto;
using Cryptocurrency.Application.Dto.ServiceResultDto;
using Cryptocurrency.Core.Entities;
using Mapster;

namespace Cryptocurrency.Application.Mapping
{
    public static class Mapper
    {
        public static CryptoListDto ToCryptoListDto(this CoinMarketAPIDto coinMarket)
        {
            return coinMarket.Adapt<CryptoListDto>();
        }

        public static CryptoPriceDto ToCryptoPriceDto(this Crypto crypto)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Crypto, CryptoPriceDto>()
                .Map(m => m.Name, s => s.CryptoName.Name)
                .Map(m => m.Symbol, s => s.CryptoName.Symbol)
                .Map(m => m.Prices, s => s.Prices.ToDictionary(d => d.Currency, d => d.Value));

            return crypto.Adapt<CryptoPriceDto>(config);
        }
    }
}
