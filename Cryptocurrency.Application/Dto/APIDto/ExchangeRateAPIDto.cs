namespace Cryptocurrency.Application.Dto.APIDto
{
    public class ExchangeRateAPIDto
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public bool Success { get; set; }
    }
}
