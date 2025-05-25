using System.Xml;
using AgendaUnit.Application.DTO.CustomerDto;
using AgendaUnit.Application.DTO.SchedulingDto;
using AgendaUnit.Application.DTO.SchedulingServiceDto;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
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
    private readonly ICompanyAppService _companyAppService;
    private readonly ICustomerAppService _customerAppService;
    private readonly ISchedulingServiceAppService _schedulingServiceAppService;
    private readonly IServiceAppService _serviceAppService;
    private readonly IUserAppService _userAppService;


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

    public async Task<SchedulingByCompanyCreatedDto> CreateByCompany(
        SchedulingByCompanyCreateDto schedulingByCompanyCreateDto)
    {
        var userId = _common.UserId;
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

        if (customer == null) throw new NotFoundException("Usuario Não encontrado");

        #endregion

        #region Verify services

        if (schedulingByCompanyCreateDto.Services.Count == 0)
        {
            throw new NotFoundException($"Serviço Não encontrado");
        }

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

            if (serviceObtained == null) throw new NotFoundException($"Serviço Não encontrado");

            serviceObtaineds.Add(serviceObtained);
        }

        #endregion

        var schedulingByCompanyCreatedDto = new SchedulingByCompanyCreatedDto();


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

            var schedulingCreatedDto =
                await Create<SchedulingCreateDto, SchedulingByCompanyCreatedDto>(schedulingCreateDto);

            double totalValue = 0;

            foreach (var service in serviceObtaineds)
            {
                var schedulingServiceCreateDto = new SchedulingServiceCreateDto
                {
                    Name = service.Name,
                    Price = service.Price,
                    Duration = service.Duration,
                    SchedulingId = schedulingCreatedDto.Id,
                    ServiceId = service.Id
                };

                var schedulingServiceCreatedDto =
                    await _schedulingServiceAppService.Create<SchedulingServiceCreateDto, SchedulingServiceCreateDto>(
                        schedulingServiceCreateDto);
                totalValue += service.Price;
            }

            var schedulingTotalValueUpdatedDto = new SchedulingTotalValueUpdateDto
            {
                Id = schedulingCreatedDto.Id,
                TotalPrice = totalValue - schedulingByCompanyCreateDto.Discount > 0
                    ? totalValue - schedulingByCompanyCreateDto.Discount
                    : 0
            };

            var schedulingUpdatedDto =
                await Update<SchedulingTotalValueUpdateDto, SchedulingTotalValueUpdatedDto>(
                    schedulingTotalValueUpdatedDto);


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

    public async Task<SchedulingByCompanyUpdatedDto> UpdateByCompany(SchedulingByCompanyUpdateDto schedulingByCompanyUpdateDto)
    {
        var userId = _common.UserId;
        var companyId = (int)_common.CompanyId;

        #region Verify is Scheduling exists
        var schedulingListDto = new SchedulingListDto
        {
            CompanyId = companyId,
            Id = schedulingByCompanyUpdateDto.Id
        };

        var schedulingListedDto = await GetAll<SchedulingListDto, SchedulingListedDto>(schedulingListDto);
        var scheduling = schedulingListedDto.Items.FirstOrDefault();

        if (scheduling == null)
        {
            throw new NotFoundException("Agendamento Não encontrado");
        }

        #endregion

        #region find Customer

        var customerListDto = new CustomerListDto
        {
            CompanyId = companyId,
            Id = schedulingByCompanyUpdateDto.CustomerId
        };

        var customerListedDto = await _customerAppService.GetAll<CustomerListDto, CustomerListedDto>(customerListDto);
        var customer = customerListedDto.Items.FirstOrDefault();

        if (customer == null) throw new NotFoundException("Usuario Não encontrado");

        #endregion

        #region Verify new services
        var listServicesObtained = await VerifyServicesList(schedulingByCompanyUpdateDto, companyId);
        #endregion

        #region Verify old services

        foreach (var service in schedulingByCompanyUpdateDto.Services)
        {
            var hasServiceDuplicated = schedulingByCompanyUpdateDto.OldServices.Exists((oldService) => service.Id == oldService.Id);

            if (hasServiceDuplicated)
            {
                throw new ConflictException("Serviço Duplicado");
            }
        }
        #endregion


        #region Remove old services if the input list is different from the retrieved schedule 

        var allSchedulingServices = scheduling.SchedulingServices.ToList();
        var oldServicesInput = schedulingByCompanyUpdateDto.OldServices;

        foreach (var oldServiceInput in oldServicesInput)
        {
            var itemToRemove = allSchedulingServices.SingleOrDefault(schedulingServices => schedulingServices.Service.Id == oldServiceInput.Id);

            if (itemToRemove != null)
                allSchedulingServices.Remove(itemToRemove);
        }


        // remove schedulingServices 
        foreach (var schedulingServices in allSchedulingServices)
        {
            var schedulingServiceDeleteDto = new SchedulingServiceDeleteDto
            {
                Id = schedulingServices.Id
            };

            await _schedulingServiceAppService.Delete<SchedulingServiceDeleteDto, SchedulingServiceDeletedDto>(schedulingServiceDeleteDto);
        }

        #endregion

        #region Convert Timespan

        var duration = XmlConvert.ToTimeSpan(schedulingByCompanyUpdateDto.Duration);

        #endregion


        // TODO: VERIFY WORKERS 

        #region what is service is duplicated ? 
        // separo os iguais com os diferentes para saber quem crio ou quem dou update ou quem excluo
        // mas pera quem disse que posso editar ? eu posso mandar o antigo mas não posso editar o antigo
        // se o mesmo serviço vier 2 ou + isso sera considerado um erro (so pode haver um serviço oq mudaria é o quantity isso resolve tbm o banco)




        #endregion

        var schedulingByCompanyUpdatedDto = new SchedulingByCompanyUpdatedDto();

        #region Update Scheduling

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var schedulingUpdateDto = new SchedulingUpdateDto
            {
                Id = schedulingByCompanyUpdateDto.Id,
                CompanyId = companyId,
                Date = schedulingByCompanyUpdateDto.Date.ToUniversalTime(),
                StatusId = (int)StatusEnum.Active,
                StaffUserId = schedulingByCompanyUpdateDto.StaffUserId,
                CustomerId = schedulingByCompanyUpdateDto.CustomerId,
                Duration = duration,
                Discount = schedulingByCompanyUpdateDto.Discount,
                Notes = schedulingByCompanyUpdateDto.Notes,
                TotalPrice = 0
            };

            var schedulingUpdatedDto = await Update<SchedulingUpdateDto, SchedulingByCompanyUpdatedDto>(schedulingUpdateDto);

            double totalValue = 0;

            foreach (var service in listServicesObtained)
            {
                var schedulingServiceCreateDto = new SchedulingServiceCreateDto
                {
                    Name = service.Name,
                    Price = service.Price,
                    Duration = service.Duration,
                    SchedulingId = schedulingUpdatedDto.Id,
                    ServiceId = service.Id
                };

                var schedulingServiceCreatedDto =
                    await _schedulingServiceAppService.Create
                        <SchedulingServiceCreateDto, SchedulingServiceCreateDto>(schedulingServiceCreateDto);

                totalValue += service.Price;
            }

            var schedulingTotalValueUpdateDto = new SchedulingTotalValueUpdateDto
            {
                Id = schedulingUpdatedDto.Id,
                TotalPrice = totalValue - schedulingByCompanyUpdateDto.Discount > 0
                    ? totalValue - schedulingByCompanyUpdateDto.Discount
                    : 0
            };

            var schedulingTotalValueUpdatedDto =
                await Update<SchedulingTotalValueUpdateDto, SchedulingTotalValueUpdatedDto>(
                    schedulingTotalValueUpdateDto);


            schedulingByCompanyUpdatedDto = schedulingUpdatedDto;
            schedulingByCompanyUpdatedDto.TotalPrice = schedulingTotalValueUpdateDto.TotalPrice;


            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw ex;
        }

        #endregion


        return schedulingByCompanyUpdatedDto;
    }

    private async Task<List<ServiceListedDto>> VerifyServicesList(SchedulingByCompanyUpdateDto schedulingByCompanyUpdateDto, int companyId)
    {
        var listServices = new List<ServiceListedDto>();

        foreach (var serviceId in schedulingByCompanyUpdateDto.Services)
        {
            var serviceListDto = new ServiceListDto
            {
                CompanyId = companyId,
                Id = serviceId.Id
            };

            var serviceListedDto = await _serviceAppService.GetAll<ServiceListDto, ServiceListedDto>(serviceListDto);
            var serviceObtained = serviceListedDto.Items.FirstOrDefault();

            if (serviceObtained == null) throw new NotFoundException($"Serviço Não encontrado");

            listServices.Add(serviceObtained);
        }

        return listServices;
    }

}