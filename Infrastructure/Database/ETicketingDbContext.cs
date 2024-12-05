using System.Linq.Expressions;
using eticketing.Models;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Infrastructure.Database;

public class ETicketingDbContext(DbContextOptions<ETicketingDbContext> options) : DbContext(options)
{
    public DbSet<Admin> Admin { get; set; } = null!;
    public DbSet<Customer> Customer { get; set; } = null!;
    public DbSet<Event> Event { get; set; } = null!;
    public DbSet<Ticket> Ticket { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        GenerateUuid<Admin>(modelBuilder, "Id");
        SoftDelete<Admin>(modelBuilder);
        DefaultCreatedAtGetDate<Admin>(modelBuilder);

        GenerateUuid<Customer>(modelBuilder, "Id");
        SoftDelete<Customer>(modelBuilder);
        DefaultCreatedAtGetDate<Customer>(modelBuilder);

        GenerateUuid<Event>(modelBuilder, "Id");
        SoftDelete<Event>(modelBuilder);
        DefaultCreatedAtGetDate<Event>(modelBuilder);

        GenerateUuid<Ticket>(modelBuilder, "Id");
        SoftDelete<Ticket>(modelBuilder);
        DefaultCreatedAtGetDate<Ticket>(modelBuilder);

        modelBuilder.Entity<Admin>().HasIndex(admin => admin.Email).IsUnique();
        modelBuilder.Entity<Customer>().HasIndex(customer => customer.Email).IsUnique();
        modelBuilder.Entity<Customer>().HasIndex(customer => customer.Username).IsUnique();
        modelBuilder.Entity<Ticket>().HasIndex(ticket => ticket.Code).IsUnique();

        modelBuilder
            .Entity<Ticket>()
            .Property(ticket => ticket.Status)
            .HasDefaultValue(eticketing.Models.Ticket.TicketStatus.Pending);
    }

    private static void GenerateUuid<T>(ModelBuilder modelBuilder, string column)
        where T : Base
    {
        modelBuilder.Entity<T>().HasIndex(CreateExpression<T>(column));

        modelBuilder.Entity<T>().Property(column).HasDefaultValueSql("NEWID()");
    }

    private static Expression<Func<T, object?>> CreateExpression<T>(string uuid)
        where T : Base
    {
        var type = typeof(T);
        var property = type.GetProperty(uuid);
        var parameter = Expression.Parameter(type);
        Expression access;
        if (property != null)
        {
            access = Expression.Property(parameter, property);
        }
        else
        {
            throw new ArgumentException($"Property '{uuid}' not found on type '{type}'.");
        }
        var convert = Expression.Convert(access, typeof(object));
        var function = Expression.Lambda<Func<T, object?>>(convert, parameter);

        return function;
    }

    private static void SoftDelete<T>(ModelBuilder modelBuilder)
        where T : Base
    {
        modelBuilder
            .Entity<T>()
            .HasQueryFilter(e => EF.Property<DateTime?>(e, "DeletedAt") == null);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is Base
                && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Modified)
            {
                ((Base)entityEntry.Entity).UpdatedAt = DateTime.Now;
            }

            if (entityEntry.State == EntityState.Added)
            {
                ((Base)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }

    private static void DefaultCreatedAtGetDate<T>(ModelBuilder modelBuilder)
        where T : Base
    {
        modelBuilder.Entity<T>().Property(t => t.CreatedAt).HasDefaultValueSql("GETDATE()");
    }
}