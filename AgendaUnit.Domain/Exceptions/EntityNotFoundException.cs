namespace AgendaUnit.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public int EntityId { get; }

    public EntityNotFoundException(int entityId)
    {
        EntityId = entityId;
    }

    public EntityNotFoundException(int entityId, string message)
        : base(message)
    {
        EntityId = entityId;
    }

    public EntityNotFoundException(int entityId, string message, Exception innerException)
        : base(message, innerException)
    {
        EntityId = entityId;
    }
}
