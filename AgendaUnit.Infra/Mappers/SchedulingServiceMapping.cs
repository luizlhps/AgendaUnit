using AgendaUnit.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infra.Mappers;

public class SchedulingServiceMapping
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SchedulingService>().ToTable(@"scheduling_service", @"public");

        modelBuilder.Entity<SchedulingService>().HasKey(ss => new { ss.SchedulingId, ss.ServiceId });

        modelBuilder.Entity<SchedulingService>().Property(x => x.ServiceId).HasColumnName(@"service_id").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.SchedulingId).HasColumnName(@"scheduling_id").IsRequired();

        modelBuilder.Entity<SchedulingService>().Property(x => x.Name).HasColumnName(@"name").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.Price).HasColumnName(@"price").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.TotalPrice).HasColumnName(@"total_price").IsRequired();
        modelBuilder.Entity<SchedulingService>().Property(x => x.Discount).HasColumnName(@"discount").IsRequired();

        modelBuilder.Entity<SchedulingService>().Property(x => x.IsDeleted).HasColumnName(@"isdeleted").HasColumnType(@"bool").IsRequired();

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
