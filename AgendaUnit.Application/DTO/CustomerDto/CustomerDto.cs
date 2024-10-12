using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CustomerDto;


[AutoMap(typeof(Customer), ReverseMap = true)]
public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; }

    public List<Scheduling> Scheduling { get; set; }
}
