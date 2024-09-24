using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerServiceCreateDto
{
    public bool? Success { get; set; }
    public virtual ServiceDto Service { get; set; }

    [AutoMap(typeof(Service), ReverseMap = true)]
    public class ServiceDto
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
    }
}

