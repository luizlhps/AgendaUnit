namespace AgendaUnit.Shared.CrossCutting;

public interface ICommon
{
    public int UserId { get; set; }
    public string UserRole { get; set; }
    public int? CompanyId { get; set; }
}
