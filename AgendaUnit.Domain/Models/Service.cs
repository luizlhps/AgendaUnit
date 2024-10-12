namespace AgendaUnit.Domain.Models;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.Models;


public class Service : BaseEntity
{


    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public decimal Price { get; set; }

    public bool Ativo { get; set; }

    [Column(name: "status_id")]
    public int StatusId { get; set; }
    public Status Status { get; set; }

    [Column(name: "company_id")]
    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public List<Scheduling> Schedulings { get; set; }
}

