using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Models;

public class Status : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Scheduling> Schedulings { get; set; }
}
