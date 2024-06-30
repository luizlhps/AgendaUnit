using AgendaUnit.Domain.Interfaces.Models;

namespace AgendaUnit.Domain.models;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }

    public Guid? Uuid { get; set; }

    public DateTime TimeStamp { get; set; }
}
