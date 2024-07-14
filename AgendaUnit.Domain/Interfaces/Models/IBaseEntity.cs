namespace AgendaUnit.Domain.Interfaces.Models;

public interface IBaseEntity
{
    public int Id { get; set; }

    public DateTimeOffset TimeStamp { get; set; }
}
