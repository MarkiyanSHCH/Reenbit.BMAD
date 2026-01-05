namespace Reenbit.BMAD.Domain.Result.Implementations
{
    internal class FailedResult : IResult
    {
        public Error Error { get; protected set; }

        public static IResult Failed(string message, Exception exception, ErrorType type)
            => new FailedResult
            {
                Error = new Error
                {
                    Message = message,
                    Type = type,
                    Exception = exception
                }
            };
    }

    internal class FailedResult<TData> : FailedResult, IResult<TData>
    {
        private const string NotSupportedMessageTemplate = "Getting {0} property is not supported for the failed result.";

        public TData Data
            => throw new NotSupportedException(string.Format(NotSupportedMessageTemplate, nameof(Data)));

        public static IResult<TData> From(IResult failedResult)
            => new FailedResult<TData> { Error = failedResult.Error };

        public static new IResult<TData> Failed(string message, Exception exception, ErrorType type)
            => new FailedResult<TData>
            {
                Error = new Error
                {
                    Message = message,
                    Type = type,
                    Exception = exception
                }
            };
    }
}