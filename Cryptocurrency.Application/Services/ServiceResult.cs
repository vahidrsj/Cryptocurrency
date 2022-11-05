using Cryptocurrency.Core.Errors;

namespace Cryptocurrency.Application.Services
{
    public class ServiceResult<TResult>
    {
        public TResult Result { get; set; }
        public bool IsSuccessfull { get; set; }

        public ErrorResultDto ErrorInfo { get; set; }

        public ServiceResult(TResult result): this(result, true, null)
        {
        }

        public ServiceResult(ErrorResultDto error) : this(result: default(TResult), isSuccessfull:false, error)
        {
        }

        public ServiceResult(TResult result, bool isSuccessfull, ErrorResultDto errorInfo)
        {
            this.Result = result;
            this.IsSuccessfull = isSuccessfull;
            this.ErrorInfo = errorInfo;
        }
    }
}
