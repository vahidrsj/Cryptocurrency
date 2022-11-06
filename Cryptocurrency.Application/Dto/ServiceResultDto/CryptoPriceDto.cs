using Mapster;

namespace Cryptocurrency.Application.Dto.ServiceResultDto
{
    public class CryptoPriceDto
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Dictionary<string, decimal> Prices { get; set; }
    }
}
