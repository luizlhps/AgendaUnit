
using AgendaUnit.Domain.Models;
using AutoMapper;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserUpdatedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}