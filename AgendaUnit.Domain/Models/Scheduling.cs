
namespace AgendaUnit.Domain.models;

public class Scheduling : BaseEntity
{
    public DateTime? Date { get; set; }

    public decimal TotalPrice { get; set; }

    public string Hours { get; set; }

    public string Notes { get; set; }

    public string Status { get; set; }

    public string? CancelNote { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public int StaffUserId { get; set; }
    public User StaffUser { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public Customer Customer { get; set; }

}