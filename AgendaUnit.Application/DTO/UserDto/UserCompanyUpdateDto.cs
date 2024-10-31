
using AgendaUnit.Domain.Models;
using AutoMapper;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserCompanyUpdateDto
{
    public int Id { get; set; }
    public int? CompanyId { get; set; }
}