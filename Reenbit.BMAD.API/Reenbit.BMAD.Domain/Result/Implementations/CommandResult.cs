namespace Reenbit.BMAD.Domain.Result.Implementations
{
    internal class CommandResult : IResult
    {
        public Error Error { get; private set; }

        public static IResult Success()
            => new CommandResult();
    }
}