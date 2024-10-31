namespace AgendaUnit.Shared.Queries.Interface;

public interface IQueryParams
{
    public IPageList PaginationProperties { get; set; }
    public IFilterParameters Filters { get; set; }
}
