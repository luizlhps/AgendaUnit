using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CustomerDto;


[AutoMap(typeof(Customer), ReverseMap = true)]
public class CustomerListedDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int CompanyId { get; set; }

    public List<SchedulingDto>? Schedulings { get; set; }


    [AutoMap(typeof(Scheduling), ReverseMap = true)]
    public class SchedulingDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int StaffUserId { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }

        public DateTimeOffset Date { get; set; }
        public string? Notes { get; set; }
        public TimeSpan Duration { get; set; }
        public string? CancelNote { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
    }

}
