namespace AgendaUnit.Shared.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message)
    {
        StatusCode = 401;
    }

}
