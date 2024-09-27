using AgendaUnit.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infra.Mappers;

public class CompanyMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().ToTable(@"company", @"public");
        modelBuilder.Entity<Company>().Property(x => x.Id).HasColumnName(@"id").HasColumnType(@"int4").IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Company>().Property(x => x.Name).HasColumnName(@"name").HasColumnType(@"text").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Company>().Property(x => x.TypeCompany).HasColumnName(@"type_company").HasColumnType(@"text").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Company>().Property(x => x.OwnerId).HasColumnName(@"owner_id").HasColumnType(@"int4").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Company>().Property(x => x.Timestamp).HasColumnName(@"timestamp").HasColumnType(@"timestamptz").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Company>().Property(x => x.IsDeleted).HasColumnName(@"isdeleted").HasColumnType(@"bool").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Company>().HasKey(@"Id");
    }
}