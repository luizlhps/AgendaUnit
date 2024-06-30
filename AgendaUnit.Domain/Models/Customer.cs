namespace AgendaUnit.Domain.models;

public class Customer : BaseEntity
{
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int CompanyId { get; set; }

    public List<Company> Company { get; set; }

    public List<Scheduling> Scheduling { get; set; }
}
