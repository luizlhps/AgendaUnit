using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CustomerDto;


[AutoMap(typeof(Customer), ReverseMap = true)]
public class CustomerListDto : QueryParams
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? CompanyId { get; set; }

}
