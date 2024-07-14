
using AgendaUnit.Shared.Queries.Interface;

namespace AgendaUnit.Shared.Queries;

public class QueryParams : IQueryParams
{
    public IPageList PaginationProperties { get; set; } = new PageList();
    public IFilterParameters? Filters { get; set; }


}
