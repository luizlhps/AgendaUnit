namespace AgendaUnit.Domain.Interfaces.Models;

public interface IBaseEntity
{
    public int Id { get; set; }

    public DateTime TimeStamp { get; set; }
}
