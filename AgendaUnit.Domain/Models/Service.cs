namespace AgendaUnit.Domain.models;

using System;


public class Service : BaseEntity
{
    public string Name { get; set; }

    public string Duration { get; set; }

    public decimal Price { get; set; }

    public string Status { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; }
}

