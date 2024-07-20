namespace AgendaUnit.Shared.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }

}
