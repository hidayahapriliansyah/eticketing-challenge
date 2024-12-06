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
            .Entity<Event>()
            .HasIndex(e => e.Status)
            .HasDatabaseName("IX_Event_Status_Published")
            .HasFilter("[status] = 1");

        modelBuilder
            .Entity<Ticket>()
            .Property(ticket => ticket.Status)
            .HasDefaultValue(eticketing.Models.Ticket.TicketStatus.Pending);

        Seeder(modelBuilder);
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

    private static void DefaultIsDeleted<T>(ModelBuilder modelBuilder)
        where T : Base
    {
        modelBuilder.Entity<T>().Property(t => t.IsDeleted).HasDefaultValue(false);
    }

    public void Seeder(ModelBuilder modelBuilder)
    {
        var adminId = Guid.NewGuid();

        modelBuilder
            .Entity<Admin>()
            .HasData(
                new Admin
                {
                    Id = adminId,
                    Email = "admin@admin.com",
                    Password = "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", // sangatrahasia
                }
            );

        var eventId1 = Guid.NewGuid();
        var eventId2 = Guid.NewGuid();
        var eventId3 = Guid.NewGuid();
        var eventId4 = Guid.NewGuid();
        var eventId5 = Guid.NewGuid();

        modelBuilder
            .Entity<Event>()
            .HasData(
                new Event
                {
                    Id = eventId1,
                    Name = "Concert Night",
                    Description = "A grand concert to enjoy the night",
                    EventDate = new DateTime(2024, 12, 25, 18, 0, 0),
                    Location = "Stadium A",
                    MaxParticipants = 500,
                    TicketPrice = 100000,
                    Status = EventStatus.Published,
                },
                new Event
                {
                    Id = eventId2,
                    Name = "Tech Expo",
                    Description = "Explore the latest tech innovations",
                    EventDate = new DateTime(2024, 11, 20, 9, 0, 0),
                    Location = "Expo Center",
                    MaxParticipants = 300,
                    TicketPrice = 100000,
                    Status = EventStatus.Unpublished,
                },
                new Event
                {
                    Id = eventId3,
                    Name = "Startup Workshop",
                    Description = "A workshop to boost your startup skills",
                    EventDate = new DateTime(2024, 11, 15, 10, 0, 0),
                    Location = "Startup Hub",
                    MaxParticipants = 150,
                    TicketPrice = 50000,
                    Status = EventStatus.Unpublished,
                },
                new Event
                {
                    Id = eventId4,
                    Name = "Yoga Retreat",
                    Description = "Relax and rejuvenate with yoga",
                    EventDate = new DateTime(2025, 1, 10, 8, 0, 0),
                    Location = "Wellness Resort",
                    MaxParticipants = 200,
                    TicketPrice = 150000,
                    Status = EventStatus.Published,
                },
                new Event
                {
                    Id = eventId5,
                    Name = "Music Festival",
                    Description = "Join us for a music extravaganza",
                    EventDate = new DateTime(2024, 12, 31, 20, 0, 0),
                    Location = "City Park",
                    MaxParticipants = 1000,
                    TicketPrice = 200000,
                    Status = EventStatus.Published,
                }
            );
    }
}
