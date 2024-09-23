using AgendaUnit.Domain.Models;

namespace AgendaUnit.Application.DTO;

public class SchedulingDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }
    public string Notes { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; }

    public string? CancelNote { get; set; }

    public decimal? TotalPrice { get; set; }

    public int StaffUserId { get; set; }
    public User StaffUser { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
