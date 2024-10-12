using AgendaUnit.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infra.Mappers;

public class SchedulingMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scheduling>().ToTable(@"scheduling", @"public");
        modelBuilder.Entity<Scheduling>().Property(x => x.Id).HasColumnName(@"id").HasColumnType(@"int4").IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Scheduling>().Property(x => x.Date).HasColumnName(@"date").HasColumnType(@"timestamptz").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.Notes).HasColumnName(@"notes").HasColumnType(@"text").ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.StatusId).HasColumnName(@"status_id").HasColumnType(@"int4").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.CancelNote).HasColumnName(@"cancel_note").HasColumnType(@"text").ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.TotalPrice).HasColumnName(@"total_price").HasColumnType(@"numeric").ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.StaffUserId).HasColumnName(@"staff_user_id").HasColumnType(@"int4").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.ServiceId).HasColumnName(@"service_id").HasColumnType(@"int4").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.CompanyId).HasColumnName(@"company_id").HasColumnType(@"int4").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.CustomerId).HasColumnName(@"customer_id").HasColumnType(@"int4").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.Timestamp).HasColumnName(@"timestamp").HasColumnType(@"timestamptz").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.IsDeleted).HasColumnName(@"isdeleted").HasColumnType(@"bool").IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Scheduling>().Property(x => x.Duration).HasColumnName(@"duration").HasColumnType(@"interval").IsRequired().ValueGeneratedOnAdd().HasPrecision(6).HasDefaultValueSql(@"'00:00:00'");
        modelBuilder.Entity<Scheduling>().HasKey(s => s.Id);

        modelBuilder.Entity<Scheduling>()
                    .HasOne(c => c.User)
                    .WithMany(s => s.Schedulings)
                    .HasForeignKey(s => s.StaffUserId);

        modelBuilder.Entity<Scheduling>()
            .HasOne(c => c.Company)
            .WithMany(s => s.Schedulings)
            .HasForeignKey(c => c.CompanyId);

        modelBuilder.Entity<Scheduling>()
            .HasOne(c => c.Service)
            .WithMany(s => s.Schedulings)
            .HasForeignKey(c => c.ServiceId);

        modelBuilder.Entity<Scheduling>()
            .HasOne(c => c.Customer)
            .WithMany(s => s.Schedulings)
            .HasForeignKey(c => c.CustomerId);

        modelBuilder.Entity<Scheduling>()
             .HasOne(c => c.Status)
             .WithOne(s => s.Scheduling)
             .HasForeignKey<Scheduling>(c => c.StatusId)
             .IsRequired();

    }
}
