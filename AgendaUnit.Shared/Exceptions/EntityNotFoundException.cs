namespace AgendaUnit.Shared.Exceptions;

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }

}
