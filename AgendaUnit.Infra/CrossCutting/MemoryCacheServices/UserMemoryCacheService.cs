
using AgendaUnit.Application.Interfaces;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace AgendaUnit.Infra.CrossCutting.MemoryCacheServices;
public class UserMemoryCacheService : IUserMemoryCacheService
{
    private readonly IMemoryCache _memoryCache;

    public UserMemoryCacheService(
        IMemoryCache memoryCache
    )
    {
        _memoryCache = memoryCache;
    }

    public UserCacheDto GetData(string? email)
    {

        return _memoryCache.TryGetValue(email, out UserCacheDto value) ? value : default;

    }

    public void RemoveData(string userId)
    {
        _memoryCache.Remove(userId);
    }

    public void SetData(UserCacheDto user)
    {
        _memoryCache.Set(user.email, user, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        });
    }


}
