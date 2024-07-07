namespace AgendaUnit.Domain.models;

using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Service : BaseEntity
{
    public string Name { get; set; }

    public string Duration { get; set; }

    public decimal Price { get; set; }

    public bool Ativo { get; set; }

    public int Status { get; set; }

    [Column(name: "company_id")]
    public int CompanyId { get; set; }

    public Company Company { get; set; }
}

