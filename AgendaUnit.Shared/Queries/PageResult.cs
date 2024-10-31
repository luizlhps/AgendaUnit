
using AgendaUnit.Shared.Queries.Interface;

namespace AgendaUnit.Shared.Queries;

public class PageResult<T> : IPageResult<T>
{
    public IList<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize) > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}
