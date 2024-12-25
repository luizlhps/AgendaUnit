using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.CustomerDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class CustomerAppService : Crud<Customer>, ICustomerAppService
{
    private readonly ICommon _common;

    public CustomerAppService(
        IUnitOfWork unitOfWork
        , IMapper mapper
        , IServiceProvider serviceProvider
        , ICommon common
        ) : base(unitOfWork, mapper, serviceProvider)
    {
        _common = common;
    }

    public async Task<PageResult<CustomerListedDto>> GetAllCustomersByCompany(CustomerListDto customerListDto)
    {
        int companyId = _common.CompanyId ?? 0;

        customerListDto.CompanyId = companyId;

        return await GetAll<CustomerListDto, CustomerListedDto>(customerListDto);
    }

}
