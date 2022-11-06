namespace Cryptocurrency.Application.Dto.APIDto
{
    public class CoinMarketAPIDataDto
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class CoinMarketAPIStatusDto
    {
        public string timestamp { get; set; }
        public int error_code { get; set; }
        public string error_message { get; set; }
    }

    public class CoinMarketAPIDto
    {
        public IEnumerable<CoinMarketAPIDataDto> Data { get; set; }
        public CoinMarketAPIStatusDto Status { get; set; }
    }
}
