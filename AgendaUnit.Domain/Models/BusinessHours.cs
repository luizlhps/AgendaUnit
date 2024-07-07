using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaUnit.Domain.models;

public class BusinessHours : BaseEntity
{
    [Column(name: "company_id")]
    public int CompanyId { get; set; }

    [Column(name: "day_of_week")]
    public string DayOfWeek { get; set; }

    [Column(name: "opening_time")]
    public DateTime OpeningTime { get; set; }

    [Column(name: "closing_time")]
    public DateTime ClosingTime { get; set; }

    public virtual Company Company { get; set; }
}
