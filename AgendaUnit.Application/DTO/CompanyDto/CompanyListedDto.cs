using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.CompanyDto;

[AutoMap(typeof(Company), ReverseMap = true)]
public class CompanyListedDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Column(name: "type_company")]
    public string TypeCompany { get; set; }

    [Column(name: "owner_id")]
    public int OwnerId { get; set; }

    public List<SchedulingDto>? Schedulings { get; set; }
    public virtual List<Customer>? Customers { get; set; }
    public virtual List<ServiceDto> Services { get; set; }
    [AutoMap(typeof(Service), ReverseMap = true)]
    public class ServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Duration { get; set; }

        public decimal Price { get; set; }

        public bool Ativo { get; set; }

    }

    [AutoMap(typeof(Customer), ReverseMap = true)]
    public class CustomerDto
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Column(name: "company_id")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
    [AutoMap(typeof(Scheduling), ReverseMap = true)]
    public class SchedulingDto
    {
        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public string Notes { get; set; }

        public int StatusId { get; set; }

        public string? CancelNote { get; set; }

        public decimal? TotalPrice { get; set; }

        public int StaffUserId { get; set; }

        public int ServiceId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

    }

}
