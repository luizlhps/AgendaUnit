using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CustomerDto;


[AutoMap(typeof(Customer), ReverseMap = true)]
public class CustomerObtainDto
{
    public int Id { get; set; }

}
