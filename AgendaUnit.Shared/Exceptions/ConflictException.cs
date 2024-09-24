namespace AgendaUnit.Shared.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(string message) : base(message)
    {
        StatusCode = 409;
    }

}