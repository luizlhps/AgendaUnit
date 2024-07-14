using Microsoft.EntityFrameworkCore;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Models;

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
        modelBuilder.Entity<Company>()
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId);

        modelBuilder.Entity<Company>().HasIndex(c => c.OwnerId).IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);


        modelBuilder.Entity<User>()
            .HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CompanyId);


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
