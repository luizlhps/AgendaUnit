using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Models;

public class Customer : BaseEntity
{
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    [Column(name: "company_id")]
    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public List<Scheduling> Scheduling { get; set; }
}
