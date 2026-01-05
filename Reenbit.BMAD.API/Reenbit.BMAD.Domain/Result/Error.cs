namespace Reenbit.BMAD.Domain.Result
{
    public class Error
    {
        public string Message { get; set; }
        public ErrorType Type { get; set; }
        public Exception Exception { get; set; }
    }
}