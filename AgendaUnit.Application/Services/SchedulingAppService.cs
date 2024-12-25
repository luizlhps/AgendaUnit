using System.Xml;
using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Application.DTO.CustomerDto;
using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Application.DTO.SchedulingServiceDto;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class SchedulingAppService : Crud<Scheduling>, ISchedulingAppService
{
    private readonly ICommon _common;
    private readonly IUserAppService _userAppService;
    private readonly ICompanyAppService _companyAppService;
    private readonly ICustomerAppService _customerAppService;
    private readonly IServiceAppService _serviceAppService;
    private readonly ISchedulingServiceAppService _schedulingServiceAppService;



    public SchedulingAppService(
        IUserAppService userAppService
        , ICommon common
        , IUnitOfWork unitOfWork
        , IMapper mapper
        , IServiceProvider serviceProvider
        , ICompanyAppService companyAppService
        , ICustomerAppService customerAppService
        , IServiceAppService serviceAppService
        , ISchedulingServiceAppService schedulingServiceAppService
        ) : base(unitOfWork, mapper, serviceProvider)
    {
        _common = common;
        _userAppService = userAppService;
        _companyAppService = companyAppService;
        _customerAppService = customerAppService;
        _serviceAppService = serviceAppService;
        _schedulingServiceAppService = schedulingServiceAppService;
    }

    public async Task<PageResult<SchedulingListedDto>> GetAllSchedulesByCompany(SchedulingListDto schedulingListDto)
    {
        var userId = _common.UserId;
        var user = await _userAppService.GetById<UserObtainedDto>(userId);

        schedulingListDto.CompanyId = (int)user.CompanyId;

        var schedules = await GetAll<SchedulingListDto, SchedulingListedDto>(schedulingListDto);

        return schedules;
    }

    public async Task<SchedulingByCompanyCreatedDto> CreateByCompany(SchedulingByCompanyCreateDto schedulingByCompanyCreateDto)
    {
        var userId = (int)_common.UserId;
        var companyId = (int)_common.CompanyId;


        #region Convert Timespan
        var duration = XmlConvert.ToTimeSpan(schedulingByCompanyCreateDto.Duration);
        #endregion


        #region find Customer

        var customerListDto = new CustomerListDto
        {
            CompanyId = companyId,
            Id = schedulingByCompanyCreateDto.CustomerId
        };

        var customerListedDto = await _customerAppService.GetAll<CustomerListDto, CustomerListedDto>(customerListDto);
        var customer = customerListedDto.Items.FirstOrDefault();

        if (customer == null)
        {
            throw new NotFoundException("Usuario Não encontrado");
        }

        #endregion

        #region Verify services
        var serviceObtaineds = new List<ServiceListedDto>();

        foreach (var serviceId in schedulingByCompanyCreateDto.Services)
        {
            var serviceListDto = new ServiceListDto
            {
                CompanyId = companyId,
                Id = serviceId.Id
            };

            var serviceListedDto = await _serviceAppService.GetAll<ServiceListDto, ServiceListedDto>(serviceListDto);
            var serviceObtained = serviceListedDto.Items.FirstOrDefault();

            if (serviceObtained == null)
            {
                throw new NotFoundException($"Serviço Não encontrado, Id: {serviceId.Id}");
            }

            serviceObtaineds.Add(serviceObtained);
        }

        #endregion

        SchedulingByCompanyCreatedDto schedulingByCompanyCreatedDto = new SchedulingByCompanyCreatedDto();


        #region Create Scheduling

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var s = schedulingByCompanyCreateDto.Date;

            var schedulingCreateDto = new SchedulingCreateDto
            {
                CompanyId = companyId,
                Date = schedulingByCompanyCreateDto.Date.ToUniversalTime(),
                StatusId = (int)StatusEnum.Active,
                StaffUserId = schedulingByCompanyCreateDto.StaffUserId,
                CustomerId = schedulingByCompanyCreateDto.CustomerId,
                Duration = duration,
                Discount = schedulingByCompanyCreateDto.Discount,
                CancelNote = schedulingByCompanyCreateDto.CancelNote,
                Notes = schedulingByCompanyCreateDto.Notes,
                TotalPrice = 0
            };

            var schedulingCreatedDto = await Create<SchedulingCreateDto, SchedulingByCompanyCreatedDto>(schedulingCreateDto);

            double totalValue = 0;

            foreach (var service in serviceObtaineds)
            {
                var schedulingServiceCreateDto = new SchedulingServiceCreateDto
                {
                    Name = "Serviço " + DateTimeOffset.UtcNow.ToString(),
                    Price = service.Price,
                    Duration = service.Duration,
                    SchedulingId = schedulingCreatedDto.Id,
                    ServiceId = service.Id
                };

                var schedulingServiceCreatedDto = await _schedulingServiceAppService.Create<SchedulingServiceCreateDto, SchedulingServiceCreateDto>(schedulingServiceCreateDto);
                totalValue += service.Price;
            }

            var schedulingTotalValueUpdatedDto = new SchedulingTotalValueUpdateDto
            {
                Id = schedulingCreatedDto.Id,
                TotalPrice = (totalValue - schedulingByCompanyCreateDto.Discount) > 0 ? totalValue - schedulingByCompanyCreateDto.Discount : 0
            };

            var schedulingUpdatedDto = await Update<SchedulingTotalValueUpdateDto, SchedulingTotalValueUpdatedDto>(schedulingTotalValueUpdatedDto);



            schedulingByCompanyCreatedDto = schedulingCreatedDto;
            schedulingByCompanyCreatedDto.TotalPrice = schedulingTotalValueUpdatedDto.TotalPrice;


            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw ex;
        }

        #endregion


        return schedulingByCompanyCreatedDto;
    }
}



