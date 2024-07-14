namespace AgendaUnit.Shared.Queries.Interface;


public interface IPageList
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool? AllRows { get; set; }
}
