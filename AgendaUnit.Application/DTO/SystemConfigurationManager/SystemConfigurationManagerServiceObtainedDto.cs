using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerServiceObtainedDto
{
    public virtual ServiceDto Services { get; set; }

    [AutoMap(typeof(Service), ReverseMap = true)]
    public class ServiceDto
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
            public string Name { get; set; }
        }

    }
}
