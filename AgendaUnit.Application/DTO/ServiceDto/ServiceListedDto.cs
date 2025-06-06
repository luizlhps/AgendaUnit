using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceListedDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public double Price { get; set; }

    public bool Ativo { get; set; }

    public StatusDto Status { get; set; }

    [AutoMap(typeof(Status), ReverseMap = true)]
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
