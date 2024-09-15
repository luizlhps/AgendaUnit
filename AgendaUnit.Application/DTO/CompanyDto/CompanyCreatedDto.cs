using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CompanyDto;

[AutoMap(typeof(Company), ReverseMap = true)]
public class CompanyCreatedDto
{

}
