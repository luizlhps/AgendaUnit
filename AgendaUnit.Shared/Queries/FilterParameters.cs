
using AgendaUnit.Shared.Queries.Interface;

namespace AgendaUnit.Shared.Queries;

public class FilterParameters : IFilterParameters
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string SearchTerm { get; set; }
}
