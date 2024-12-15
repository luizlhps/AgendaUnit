using AgendaUnit.Shared.CrossCutting;
using Microsoft.AspNetCore.Http;

namespace AgendaUnit.Shared;

public class Common : ICommon
{
    public int UserId { get; set; }
    public string UserRole { get; set; }
    public int? CompanyId { get; set; }

}
