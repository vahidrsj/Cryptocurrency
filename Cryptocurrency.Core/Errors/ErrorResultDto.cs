using Cryptocurrency.Core.Enums;

namespace Cryptocurrency.Core.Errors
{
    public class ErrorResultDto
    {
        public ErrorTypes ErrorType { get; set; }
        public string Message { get; set; }
    }
}
