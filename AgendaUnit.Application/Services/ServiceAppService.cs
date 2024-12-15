using System.Xml;
using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class ServiceAppService : Crud<Service>, IServiceAppService
{

    private readonly ICommon _common;
    private readonly IUserAppService _userAppService;


    public ServiceAppService(IUserAppService userAppService
        , ICommon common, IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
    {
        _common = common;
        _userAppService = userAppService;
    }

    public async Task<ServiceByCompanyCreatedDto> CreateByCompany(ServiceByCompanyCreateDto serviceByCompanyCreateDto)
    {

        #region Convert Timespan
        var duration = XmlConvert.ToTimeSpan(serviceByCompanyCreateDto.Duration);
        #endregion

        var serviceCreateDto = new ServiceCreateDto
        {
            CompanyId = _common.CompanyId.Value,
            Duration = duration,
            Name = serviceByCompanyCreateDto.Name,
            Price = serviceByCompanyCreateDto.Price,
            StatusId = (int)StatusEnum.Active
        };

        return await Create<ServiceCreateDto, ServiceByCompanyCreatedDto>(serviceCreateDto);
    }

    public async Task<PageResult<ServiceListedDto>> GetAllByCompany(ServiceListDto serviceListDto)
    {
        var userId = _common.UserId;
        var user = await _userAppService.GetById<UserObtainedDto>(userId);

        serviceListDto.CompanyId = (int)user.CompanyId;

        var servicesListedDto = await GetAll<ServiceListDto, ServiceListedDto>(serviceListDto);

        return servicesListedDto;
    }


}
