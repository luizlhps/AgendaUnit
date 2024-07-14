using AgendaUnit.Domain.Models;

namespace AgendaUnit.Application.DTO;

public class BusinessHoursDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string DayOfWeek { get; set; }

    public DateTime OpeningTime { get; set; }

    public DateTime ClosingTime { get; set; }

    public virtual Company Company { get; set; }
}
