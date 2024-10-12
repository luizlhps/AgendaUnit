using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CustomerDto;


[AutoMap(typeof(Customer), ReverseMap = true)]
public class CustomerCreatedDto
{
    public int Id { get; set; }

}
