namespace AgendaUnit.Shared.Queries.Interface;

public interface IFilterParameters
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string SearchTerm { get; set; }
}
