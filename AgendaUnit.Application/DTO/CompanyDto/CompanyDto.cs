using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CompanyDto;

[AutoMap(typeof(Company), ReverseMap = true)]
public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string TypeCompany { get; set; }

    public int OwnerId { get; set; }

    public User Owner { get; set; }

    public List<User>? Users { get; set; }

    public List<Customer>? Customers { get; set; }

    public List<Scheduling>? Scheduling { get; set; }

    public List<Service>? Services { get; set; }
}
