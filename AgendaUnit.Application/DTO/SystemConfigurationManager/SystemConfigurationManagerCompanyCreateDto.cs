using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerCompanyCreateDto
{
    public virtual CompanyDto Company { get; set; }

    [AutoMap(typeof(Company), ReverseMap = true)]
    public class CompanyDto
    {
        public string Name { get; set; }
        public string TypeCompany { get; set; }
    }
}

