using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CompanyDto;

[AutoMap(typeof(Company), ReverseMap = true)]
public class CompanyListDto : QueryParams
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? TypeCompany { get; set; }

    public int? OwnerId { get; set; }

}
