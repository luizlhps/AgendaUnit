using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Models;

public class Status : BaseEntity
{
    public string Name { get; set; }

    public virtual Scheduling Scheduling { get; set; }
}
