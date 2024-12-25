
using AgendaUnit.Shared.Queries.Interface;

namespace AgendaUnit.Shared.Queries;

public class FilterParameters : IFilterParameters
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string SearchTerm { get; set; }
}
