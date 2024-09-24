using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.Enums;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Utils.ValidatorHelper;

namespace AgendaUnit.Application.Services.Managers;

public class SystemConfigurationManagerService : ISystemConfigurationManagerService
{
    private readonly ICommon _common;
    private readonly ICompanyAppService _companyAppService;
    private readonly IServiceAppService _serviceAppService;
    private readonly IServiceProvider _serviceProvider;

    public SystemConfigurationManagerService(ICommon common, IServiceProvider serviceProvider, IServiceAppService serviceAppService, ICompanyAppService companyService)
    {
        _common = common;
        _companyAppService = companyService;
        _serviceAppService = serviceAppService;
        _serviceProvider = serviceProvider;
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

        var systemConfigurationManagerCompanyCreatedDto = await _companyAppService.Create<CompanyCreateDto, SystemConfigurationManagerCompanyCreatedDto>(companyCreateDto);

        return systemConfigurationManagerCompanyCreatedDto;
    }

    public async Task<SystemConfigurationManagerServiceCreatedDto> CreateService(SystemConfigurationManagerServiceCreateDto systemConfigurationManagerServiceCreateDto)
    {
        ValidatorHelper.Validate(systemConfigurationManagerServiceCreateDto, _serviceProvider);

        var userId = _common.UserId;

        var companyCreateDto = new CompanyListDto
        {
            OwnerId = userId,
        };

        var companyListedDto = await _companyAppService.GetAll<CompanyListDto, CompanyListedDto>(companyCreateDto);

        var company = companyListedDto.Items.FirstOrDefault();

        if (company == null)
        {
            throw new NotFoundException("Você não possui uma empresa cadastrada.");
        }

        if (company.Services.Count > 0)
        {
            throw new ConflictException("Já existe um serviço para esta configuração");
        };

        var serviceCreateDto = new ServiceCreateDto
        {
            Name = systemConfigurationManagerServiceCreateDto.Service.Name,
            CompanyId = companyListedDto.Items.First().Id,
            Price = systemConfigurationManagerServiceCreateDto.Service.Price,
            StatusId = (int)StatusEnum.Active,
            Duration = systemConfigurationManagerServiceCreateDto.Service.Duration

        };


        var serviceCreated = await _serviceAppService.Create<ServiceCreateDto, ServiceCreatedDto>(serviceCreateDto);

        var systemConfigurationManagerServiceCreatedDto = new SystemConfigurationManagerServiceCreatedDto
        {
            Success = true
        };

        return systemConfigurationManagerServiceCreatedDto;
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
