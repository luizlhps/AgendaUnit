using Microsoft.EntityFrameworkCore;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra.Mappers;

namespace AgendaUnit.Infra.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> User { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<Scheduling> Scheduling { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Status> Status { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        SchedulingMapping.Configure(modelBuilder);
        CompanyMapping.Configure(modelBuilder);
        SchedulingServiceMapping.Configure(modelBuilder);

        modelBuilder.Entity<Service>().HasQueryFilter(p => !p.IsDeleted)
            .HasOne(c => c.Company)
            .WithMany(s => s.Services)
            .HasForeignKey(s => s.CompanyId)
            .IsRequired();

        modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted)
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted)
            .HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CompanyId);

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();


        modelBuilder.Entity<Service>().Property(s => s.Duration).HasColumnType("INTERVAL");


        base.OnModelCreating(modelBuilder);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName().ToLower());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());

            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName().ToLower());
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                foreignKey.SetConstraintName(foreignKey.GetConstraintName().ToLower());
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName().ToLower());
            }
        }
    }
}
