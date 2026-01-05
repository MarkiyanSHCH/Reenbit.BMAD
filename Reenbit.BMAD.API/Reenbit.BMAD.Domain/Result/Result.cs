using Reenbit.BMAD.Domain.Result.Implementations;

namespace Reenbit.BMAD.Domain.Result
{
    public static class Result
    {
        public static IResult Success()
            => CommandResult.Success();
        public static IResult<TData> Success<TData>(TData data)
            => QueryResult<TData>.Success(data);

        public static IResult From(IResult failedResult)
            => failedResult;
        public static IResult<TData> From<TData>(IResult failedResult)
            => FailedResult<TData>.From(failedResult);

        public static IResult Failed(
            string message,
            Exception exception = null,
            ErrorType type = ErrorType.Unprocessable)
            => FailedResult.Failed(message, exception, type);
        public static IResult<TData> Failed<TData>(
            string message,
            Exception exception = null,
            ErrorType type = ErrorType.Unprocessable)
            => FailedResult<TData>.Failed(message, exception, type);

        public static IResult Forbidden(string message, Exception exception = null)
            => FailedResult.Failed(message, exception, ErrorType.Forbidden);
        public static IResult<TData> Forbidden<TData>(string message, Exception exception = null)
            => FailedResult<TData>.Failed(message, exception, ErrorType.Forbidden);

        public static IResult BadRequest(string message, Exception exception = null)
            => FailedResult.Failed(message, exception, ErrorType.BadRequest);
        public static IResult<TData> BadRequest<TData>(string message, Exception exception = null)
            => FailedResult<TData>.Failed(message, exception, ErrorType.BadRequest);

        public static IResult NotFound(string message, Exception exception = null)
            => FailedResult.Failed(message, exception, ErrorType.NotFound);
        public static IResult<TData> NotFound<TData>(string message, Exception exception = null)
            => FailedResult<TData>.Failed(message, exception, ErrorType.NotFound);

        public static IResult Conflict(string message, Exception exception = null)
            => FailedResult.Failed(message, exception, ErrorType.Conflict);
        public static IResult<TData> Conflict<TData>(string message, Exception exception = null)
            => FailedResult<TData>.Failed(message, exception, ErrorType.Conflict);
    }
}