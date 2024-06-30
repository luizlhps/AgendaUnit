namespace AgendaUnit.Domain.models;

public class BusinessHours : BaseEntity
{
    public int CompanyId { get; set; }

    public string DayOfWeek { get; set; }

    public DateTime OpeningTime { get; set; }

    public DateTime ClosingTime { get; set; }

    public virtual Company Company { get; set; }
}
