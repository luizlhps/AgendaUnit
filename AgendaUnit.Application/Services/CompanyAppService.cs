using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class CompanyAppService : Crud<Company>, ICompanyAppService
{
    public CompanyAppService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
    {

    }

    async public override Task<Company> CreateExec(Company entity)
    {

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.BaseRepository<Company>().Create(entity);
            await _unitOfWork.Commit();

            var user = await _unitOfWork.BaseRepository<User>().GetById(entity.OwnerId);
            user.CompanyId = entity.Id;

            await _unitOfWork.BaseRepository<User>().Update(user);

            await _unitOfWork.Commit();

            await _unitOfWork.CommitTransactionAsync();

            return entity;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
