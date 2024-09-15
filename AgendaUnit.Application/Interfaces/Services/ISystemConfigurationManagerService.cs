using AgendaUnit.Application.DTO.SystemConfigurationManager;

namespace AgendaUnit.Application.Interfaces.Services;

public interface ISystemConfigurationManagerService
{
    Task<SystemConfigurationManagerCompanyCreatedDto> CreateCompany(SystemConfigurationManagerCompanyCreateDto systemConfigurationManagerCompanyCreateDto);
    Task<SystemConfigurationManagerVerifiedDto> VerifyAccountConfiguration();
}
