namespace AgendaUnit.Domain.Interfaces.Models;

public interface IBaseEntity
{
    public int Id { get; set; }

    public Guid? Uuid { get; set; }

    public DateTime TimeStamp { get; set; }
}
