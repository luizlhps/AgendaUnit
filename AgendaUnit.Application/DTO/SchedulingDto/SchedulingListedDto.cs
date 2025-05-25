using System.Text.Json.Serialization;
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingListedDto
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



    public virtual StatusDto Status { get; set; }
    public virtual CustomerDto Customer { get; set; }
    public List<SchedulingServiceDto> SchedulingServices { get; set; }
    public virtual UserDto User { get; set; }

    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public RoleDto Role { get; set; }

        public string Phone { get; set; }


        [AutoMap(typeof(Role), ReverseMap = true)]
        public class RoleDto
        {
            public string Name { get; set; }

        }

    }

    [AutoMap(typeof(Customer), ReverseMap = true)]
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }


    [AutoMap(typeof(SchedulingService), ReverseMap = true)]
    public class SchedulingServiceDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int SchedulingId { get; set; }

        public string Name { get; set; }

        public TimeSpan Duration { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }


        public virtual ServiceDto Service { get; set; }
        [AutoMap(typeof(Service), ReverseMap = true)]
        public class ServiceDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public TimeSpan Duration { get; set; }

            public double Price { get; set; }

            public bool Ativo { get; set; }

            public int StatusId { get; set; }

            public virtual StatusDto Status { get; set; }

        }

    }
    [AutoMap(typeof(Status), ReverseMap = true)]
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
