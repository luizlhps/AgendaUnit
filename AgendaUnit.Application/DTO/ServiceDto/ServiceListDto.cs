using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceListDto : QueryParams
{
    public int? CompanyId { get; set; }
}
