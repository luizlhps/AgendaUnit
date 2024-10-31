using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Dtos;

namespace AgendaUnit.Application.Interfaces;
public interface IUserMemoryCacheService
{
    public UserCacheDto GetData(string? email);
    public void SetData(UserCacheDto value);
    public void RemoveData(string key);
}
