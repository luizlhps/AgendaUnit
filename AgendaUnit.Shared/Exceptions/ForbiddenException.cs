namespace AgendaUnit.Shared.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(message)
    {
        StatusCode = 403;
    }

}
