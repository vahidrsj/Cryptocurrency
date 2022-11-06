using Cryptocurrency.Core.Enums;

namespace Cryptocurrency.Core.Errors
{
    public class ErrorResultDto
    {
        public ErrorTypes ErrorType { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            switch (ErrorType)
            {
                case ErrorTypes.None:
                    return string.Empty;
                case ErrorTypes.InvalidParameter:
                    return "Parameter is empty or not valid";
                case ErrorTypes.APICallError:
                    return $"API call error: \r\n {Message}";
                case ErrorTypes.APIReturnError:
                    return $"API return error: \r\n {Message}";
                case ErrorTypes.NotFound:
                    return "Crypto symbol not found";
                default:
                    return string.Empty;
            }
        }
    }
}
