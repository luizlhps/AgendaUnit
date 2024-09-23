using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.Enums;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgendaUnit.Application.Services.Managers;

public class SystemConfigurationManagerService : ISystemConfigurationManagerService
{
    private readonly ICommon _common;
    private readonly ICompanyAppService _companyAppService;
    private readonly IServiceAppService _serviceAppService;

    public SystemConfigurationManagerService(ICommon common, IServiceAppService serviceAppService, ICompanyAppService companyService)
    {
        _common = common;
        _companyAppService = companyService;
        _serviceAppService = serviceAppService;
    }

    public async Task<SystemConfigurationManagerCompanyCreatedDto> CreateCompany(SystemConfigurationManagerCompanyCreateDto systemConfigurationManagerCompanyCreateDto)
    {
        var userId = _common.UserId;

        var companyCreateDto = new CompanyCreateDto
        {
            Name = systemConfigurationManagerCompanyCreateDto.Company.Name,
            OwnerId = userId,
            TypeCompany = systemConfigurationManagerCompanyCreateDto.Company.TypeCompany,
        };

        var systemConfigurationManagerCompanyCreatedDto = await _companyAppService.Create<CompanyCreateDto, SystemConfigurationManagerCompanyCreatedDto>(companyCreateDto, true);

        return systemConfigurationManagerCompanyCreatedDto;
    }

    public async Task<SystemConfigurationManagerVerifiedDto> VerifyAccountConfiguration()
    {

        SystemConfigurationManagerVerifiedDto SystemConfigurationManagerVerifiedDto;

        if (_common.UserRole == nameof(RoleEnum.Admin))
        {
            #region Verify if user already has a create company
            var companyListDto = new CompanyListDto
            {
                OwnerId = _common.UserId
            };

            var companyListedDto = await _companyAppService.GetAll<CompanyListDto, CompanyListedDto>(companyListDto);
            var company = companyListedDto.Items.FirstOrDefault();

            if (company == null)
            {
                SystemConfigurationManagerVerifiedDto = new SystemConfigurationManagerVerifiedDto
                {
                    SystemConfigurated = false,
                    Step = SystemConfigurationStepEnum.CompanyNotCreated,
                };

                return SystemConfigurationManagerVerifiedDto;
            }

            #endregion 
            #region Verify if company already has at least one scheduling customer 

            if (company.Scheduling?.Count == 0)
            {
                SystemConfigurationManagerVerifiedDto = new SystemConfigurationManagerVerifiedDto
                {
                    SystemConfigurated = false,
                    Step = SystemConfigurationStepEnum.SchedulingNotCreated,
                };

                return SystemConfigurationManagerVerifiedDto;
            }

            #endregion 

        }


        SystemConfigurationManagerVerifiedDto = new SystemConfigurationManagerVerifiedDto
        {
            SystemConfigurated = true,
        };

        return SystemConfigurationManagerVerifiedDto;

    }

}
