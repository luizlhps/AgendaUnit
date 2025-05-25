using AgendaUnit.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infra.Mappers;

public class SchedulingServiceMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scheduling>().HasKey(s => s.Id);

        modelBuilder.Entity<Scheduling>().Property(x => x.Id).HasColumnName(@"id").HasColumnType(@"int4").IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<SchedulingService>().ToTable(@"scheduling_service", @"public");

        modelBuilder.Entity<SchedulingService>().Property(x => x.ServiceId).HasColumnName(@"service_id").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.SchedulingId).HasColumnName(@"scheduling_id").IsRequired();

        modelBuilder.Entity<SchedulingService>().Property(x => x.Name).HasColumnName(@"name").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.Price).HasColumnName(@"price").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.Duration).HasColumnName(@"duration").HasColumnType(@"interval").IsRequired();

        modelBuilder.Entity<SchedulingService>().Property(x => x.IsDeleted).HasColumnName(@"isdeleted").HasColumnType(@"bool").HasDefaultValue(true).IsRequired();

        modelBuilder.Entity<SchedulingService>().HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<SchedulingService>()
            .HasOne(c => c.Service)
            .WithMany()
            .HasForeignKey(c => c.ServiceId);

        modelBuilder.Entity<SchedulingService>()
            .HasOne(c => c.Scheduling)
            .WithMany(s => s.SchedulingServices)
            .HasForeignKey(c => c.SchedulingId);
    }

}
