﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eticketing.Infrastructure.Database;

#nullable disable

namespace ETicketing.Migrations
{
    [DbContext(typeof(ETicketingDbContext))]
    [Migration("20241206034417_SeederCustomerData")]
    partial class SeederCustomerData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eticketing.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.ToTable("admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eeb1e7d9-8928-40d3-a4e9-dc50c4e27508"),
                            Email = "admin@admin.com",
                            Password = "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744"
                        });
                });

            modelBuilder.Entity("eticketing.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("84c308f3-4710-49c3-8d6a-0da1a936058d"),
                            Email = "customer@customer.com",
                            Name = "John Doe",
                            Password = "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744",
                            Username = "cusomer"
                        });
                });

            modelBuilder.Entity("eticketing.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("additional_info");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("event_date");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("location");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int")
                        .HasColumnName("max_participants");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<int>("TicketPrice")
                        .HasColumnType("int")
                        .HasColumnName("ticket_price");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Status")
                        .HasDatabaseName("IX_Event_Status_Published")
                        .HasFilter("[status] = 1");

                    b.ToTable("events");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0814b305-8376-4b56-a6e7-3f6bd8b2ec24"),
                            Description = "A grand concert to enjoy the night",
                            EventDate = new DateTime(2024, 12, 25, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Stadium A",
                            MaxParticipants = 500,
                            Name = "Concert Night",
                            Status = 0,
                            TicketPrice = 100000
                        },
                        new
                        {
                            Id = new Guid("425f6311-5d66-4dc5-a3f2-3d01fc74750f"),
                            Description = "Explore the latest tech innovations",
                            EventDate = new DateTime(2024, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Expo Center",
                            MaxParticipants = 300,
                            Name = "Tech Expo",
                            Status = 1,
                            TicketPrice = 100000
                        },
                        new
                        {
                            Id = new Guid("76153e54-cc9d-4a90-8659-538a32688a4b"),
                            Description = "A workshop to boost your startup skills",
                            EventDate = new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Startup Hub",
                            MaxParticipants = 150,
                            Name = "Startup Workshop",
                            Status = 1,
                            TicketPrice = 50000
                        },
                        new
                        {
                            Id = new Guid("93d6f2ae-4bd3-4c2c-af88-2b002713ec87"),
                            Description = "Relax and rejuvenate with yoga",
                            EventDate = new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Wellness Resort",
                            MaxParticipants = 200,
                            Name = "Yoga Retreat",
                            Status = 0,
                            TicketPrice = 150000
                        },
                        new
                        {
                            Id = new Guid("c6222bdc-d123-4e34-98c9-242b63645fef"),
                            Description = "Join us for a music extravaganza",
                            EventDate = new DateTime(2024, 12, 31, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "City Park",
                            MaxParticipants = 1000,
                            Name = "Music Festival",
                            Status = 0,
                            TicketPrice = 200000
                        });
                });

            modelBuilder.Entity("eticketing.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("code");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("EventId");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("eticketing.Models.Ticket", b =>
                {
                    b.HasOne("eticketing.Models.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eticketing.Models.Customer", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("eticketing.Models.Customer", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("eticketing.Models.Event", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
