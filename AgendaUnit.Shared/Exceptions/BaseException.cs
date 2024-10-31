namespace AgendaUnit.Shared.Exceptions;

public class BaseException : Exception
{
    public int StatusCode { get; set; }

    public BaseException(string message) : base(message)
    {
        StatusCode = 500;
    }
}
