namespace Reenbit.BMAD.Domain.Result
{
    public interface IResult
    {
        Error Error { get; }
        bool IsSuccessful => Error == null;
        bool IsFailed => !IsSuccessful;
        bool IsUnprocessable => IsFailed && Error.Type == ErrorType.Unprocessable;
        bool IsForbidden => IsFailed && Error.Type == ErrorType.Forbidden;
        bool IsBadRequest => IsFailed && Error.Type == ErrorType.BadRequest;
        bool IsNotFound => IsFailed && Error.Type == ErrorType.NotFound;
        bool IsConflict => IsFailed && Error.Type == ErrorType.Conflict;
    }

    public interface IResult<TData> : IResult
    {
        TData Data { get; }
        TData Unwrap(TData defaultData = default)
            => IsSuccessful ? Data : defaultData;

        IResult<TData> Map(Func<TData, TData> mapper)
            => IsFailed
                ? this
                : Result.Success(mapper(Data));
        IResult<TData> MapWhen(bool predicate, Func<TData, TData> mapper)
            => !predicate
                ? this
                : Map(mapper);
    }
}