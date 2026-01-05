namespace Reenbit.BMAD.Domain.Result.Implementations
{
    internal class QueryResult<TData> : IResult<TData>
    {
        public TData Data { get; private set; }

        public Error Error { get; private set; }

        public static IResult<TData> Success(TData data = default)
            => new QueryResult<TData> { Data = data };
    }
}