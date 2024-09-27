using System.ComponentModel;
using System.Runtime.CompilerServices;
using AgendaUnit.Domain.Interfaces.Models;

namespace AgendaUnit.Domain.Models;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
}

