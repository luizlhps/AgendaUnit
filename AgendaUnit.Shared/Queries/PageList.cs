using AgendaUnit.Shared.Queries.Interface;
namespace AgendaUnit.Shared.Queries;

public class PageList : IPageList
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool? AllRows { get; set; }

}
