using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CompanyDto;

[AutoMap(typeof(Company), ReverseMap = true)]
public class CompanyCreateDto
{
    public string Name { get; set; }
    public string TypeCompany { get; set; }
    public int OwnerId { get; set; }
}
