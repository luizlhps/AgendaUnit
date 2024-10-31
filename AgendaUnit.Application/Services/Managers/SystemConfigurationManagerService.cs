using System.Xml;
using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Application.Enums;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Utils.ValidatorHelper;
using AgendaUnit.Application.DTO.CustomerDto;
using AgendaUnit.Domain.Interfaces.Context;

namespace AgendaUnit.Application.Services.Managers;

public class SystemConfigurationManagerService : ISystemConfigurationManagerService
{
    private readonly ICommon _common;
    private readonly ICompanyAppService _companyAppService;
    private readonly IServiceAppService _serviceAppService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork __unitOfWork;
    private readonly ICustomerAppService _customerAppService;
    private readonly ISchedulingAppService _schedulingAppService;
    private readonly IUserAppService _userAppService;

    public SystemConfigurationManagerService(IUserAppService userAppService, ISchedulingAppService schedulingAppService, ICustomerAppService customerAppService, IUnitOfWork unitOfWork, ICommon common, IServiceProvider serviceProvider, IServiceAppService serviceAppService, ICompanyAppService companyService)
    {
        _schedulingAppService = schedulingAppService;
        _customerAppService = customerAppService;
        _userAppService = userAppService;
        __unitOfWork = unitOfWork;
        _common = common;
        _companyAppService = companyService;
        _serviceAppService = serviceAppService;
        _serviceProvider = serviceProvider;
    }

    public async Task<SystemConfigurationManagerCompanyCreatedDto> CreateCompany(SystemConfigurationManagerCompanyCreateDto systemConfigurationManagerCompanyCreateDto)
    {
        var userId = _common.UserId;

        var companyListDto = new CompanyListDto
        {
            OwnerId = _common.UserId
        };

        var companyListedDto = await _companyAppService.GetAll<CompanyListDto, CompanyListedDto>(companyListDto);
        var company = companyListedDto.Items.FirstOrDefault();

        if (company != null)
        {
            throw new ConflictException("Você já possui uma empresa cadastrada.");
        }

        // adicionar transação no repo para criação de company e vinculo de usuario para aquele empresa
        var companyCreateDto = new CompanyCreateDto
        {
            Name = systemConfigurationManagerCompanyCreateDto.Company.Name,
            OwnerId = userId,
            TypeCompany = systemConfigurationManagerCompanyCreateDto.Company.TypeCompany,
        };


        var companyCreatedDto = await _companyAppService.Create<CompanyCreateDto, CompanyCreatedDto>(companyCreateDto, true);

        var systemConfigurationManagerCompanyCreatedDto = new SystemConfigurationManagerCompanyCreatedDto
        {
        };

        return systemConfigurationManagerCompanyCreatedDto;
    }
    public async Task<SystemConfigurationManagerServiceCreatedDto> CreateService(SystemConfigurationManagerServiceCreateDto systemConfigurationManagerServiceCreateDto)
    {

        ValidatorHelper.Validate(systemConfigurationManagerServiceCreateDto, _serviceProvider);

        var duracao = XmlConvert.ToTimeSpan(systemConfigurationManagerServiceCreateDto.Duration);
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
            Name = systemConfigurationManagerServiceCreateDto.Name,
            CompanyId = companyListedDto.Items.First().Id,
            Price = systemConfigurationManagerServiceCreateDto.Price,
            StatusId = (int)StatusEnum.Active,
            Duration = duracao

        };


        var serviceCreated = await _serviceAppService.Create<ServiceCreateDto, ServiceCreatedDto>(serviceCreateDto);

        var systemConfigurationManagerServiceCreatedDto = new SystemConfigurationManagerServiceCreatedDto
        {
            Success = true
        };

        return systemConfigurationManagerServiceCreatedDto;
    }
    public async Task<SystemConfigurationManagerServiceObtainedDto> ObtainService()
    {
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

        var serviceListDto = new ServiceListDto
        {
            CompanyId = company.Id
        };

        var serviceListedDto = await _serviceAppService.GetAll<ServiceListDto, ServiceListedDto>(serviceListDto);
        var service = serviceListedDto.Items.FirstOrDefault();

        if (service == null)
        {
            throw new NotFoundException("Você não possui um serviço cadastrado para esta empresa.");
        }

        var systemConfigurationManagerServiceObtainedDto = new SystemConfigurationManagerServiceObtainedDto
        {
            Services = new SystemConfigurationManagerServiceObtainedDto.ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Duration = service.Duration,
                Price = service.Price,
                Ativo = service.Ativo,
                Status = new SystemConfigurationManagerServiceObtainedDto.ServiceDto.StatusDto
                {
                    Name = service.Status.Name
                }
            }
        };

        return systemConfigurationManagerServiceObtainedDto;
    }
    public async Task<SystemConfigurationManagerSchedulingCreatedDto> CreateScheduling(SystemConfigurationManagerSchedulingCreateDto systemConfigurationManagerSchedulingCreateDto)
    {
        var userId = _common.UserId;

        var companyListDto = new CompanyListDto
        {
            OwnerId = userId,
        };

        var companyListedDto = await _companyAppService.GetAll<CompanyListDto, CompanyListedDto>(companyListDto);

        var company = companyListedDto.Items.FirstOrDefault();

        if (company == null)
        {
            throw new NotFoundException("Você não possui uma empresa cadastrada.");
        }

        if (company.Schedulings?.Count >= 1)
        {
            throw new NotFoundException("Você possui um agendamento cadastrado");
        }

        #region Convert Timespan
        var duration = XmlConvert.ToTimeSpan(systemConfigurationManagerSchedulingCreateDto.Scheduling.Duration);
        #endregion


        #region Create Customer

        var customerCreateDto = new CustomerCreateDto
        {
            CompanyId = company.Id,
            Email = systemConfigurationManagerSchedulingCreateDto.Customer.Email,
            Name = systemConfigurationManagerSchedulingCreateDto.Customer.Name,
            Phone = systemConfigurationManagerSchedulingCreateDto.Customer.Phone
        };

        var customerCreatedDto = await _customerAppService.Create<CustomerCreateDto, CustomerCreatedDto>(customerCreateDto);

        #endregion

        #region Create Scheduling

        var schedulingCreateDto = new SchedulingCreateDto
        {
            CompanyId = company.Id,
            Date = systemConfigurationManagerSchedulingCreateDto.Scheduling.Date,
            ServiceId = company.Services.FirstOrDefault().Id,
            StatusId = (int)StatusEnum.Active,
            StaffUserId = userId,
            TotalPrice = systemConfigurationManagerSchedulingCreateDto.Scheduling.TotalPrice,
            CustomerId = customerCreatedDto.Id,
            Duration = duration,
        };

        var schedulingCreatedDto = await _schedulingAppService.Create<SchedulingCreateDto, SchedulingCreatedDto>(schedulingCreateDto);

        #endregion



        var systemConfigurationManagerSchedulingCreatedDto = new SystemConfigurationManagerSchedulingCreatedDto
        {
        };

        return systemConfigurationManagerSchedulingCreatedDto;
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

            if (company.Schedulings?.Count == 0)
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
