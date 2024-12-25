using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using AgendaUnit.Domain.Interfaces.Models;

namespace AgendaUnit.Domain.Models;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
}

