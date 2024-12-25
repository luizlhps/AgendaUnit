namespace AgendaUnit.Shared.Queries.Interface;

public interface IFilterParameters
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string SearchTerm { get; set; }
}
