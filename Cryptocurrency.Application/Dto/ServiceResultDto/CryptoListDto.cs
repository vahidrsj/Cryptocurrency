namespace Cryptocurrency.Application.Dto.ServiceResultDto
{
    public class CryptoListDataDto
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class CryptoListDto
    {
        public IEnumerable<CryptoListDataDto> Data { get; set; }
    }
}
