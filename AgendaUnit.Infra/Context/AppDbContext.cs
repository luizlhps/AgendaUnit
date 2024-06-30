using Microsoft.EntityFrameworkCore;
using AgendaUnit.Domain.models;

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
    public DbSet<BusinessHours> BusinessHour { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);


        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Configura o nome da tabela
            entity.SetTableName(entity.GetTableName().ToLower());

            // Configura o nome das colunas
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());
            }

            // Configura o nome das chaves (Primary Key)
            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName().ToLower());
            }

            // Configura o nome das chaves estrangeiras (Foreign Keys)
            foreach (var foreignKey in entity.GetForeignKeys())
            {
                foreignKey.SetConstraintName(foreignKey.GetConstraintName().ToLower());
            }

            // Configura o nome dos índices
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName().ToLower());
            }
        }
    }
}
