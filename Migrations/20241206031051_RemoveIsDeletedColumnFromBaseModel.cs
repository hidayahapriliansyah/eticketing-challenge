using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETicketing.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsDeletedColumnFromBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("afd1b808-9dc4-44e0-ae2b-572920b7e13c"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("17de3bc4-d6d1-45d6-8359-4e0662771989"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("497da941-5daf-49c5-a9e7-f7effbf3b914"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("89dc0337-32fc-44ea-bb34-fc2e90fc070e"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("8a6cc8b4-839e-473f-ab24-f0a4fd9e6f39"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("ee660b6d-ca3d-489c-a1fd-025a80585802"));

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "events");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "admins");

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "password", "updated_at" },
                values: new object[] { new Guid("19a214ae-ad94-4c2d-bc84-f38a190494c5"), null, "admin@admin.com", "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "additional_info", "deleted_at", "description", "event_date", "location", "max_participants", "name", "status", "ticket_price", "updated_at" },
                values: new object[,]
                {
                    { new Guid("1c25fb0b-367d-4875-a1e5-2bb254ac367d"), null, null, "Relax and rejuvenate with yoga", new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "Wellness Resort", 200, "Yoga Retreat", 0, 150000, null },
                    { new Guid("524b2d29-ec13-47bc-8758-c7c5a6720074"), null, null, "A grand concert to enjoy the night", new DateTime(2024, 12, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), "Stadium A", 500, "Concert Night", 0, 100000, null },
                    { new Guid("661b5b70-5079-45c9-814e-b7f4a48c94f7"), null, null, "Join us for a music extravaganza", new DateTime(2024, 12, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), "City Park", 1000, "Music Festival", 0, 200000, null },
                    { new Guid("c202e143-0117-4580-bdca-034c86415393"), null, null, "Explore the latest tech innovations", new DateTime(2024, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "Expo Center", 300, "Tech Expo", 1, 100000, null },
                    { new Guid("dc5a170e-49bd-4af5-8198-4398207e7861"), null, null, "A workshop to boost your startup skills", new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Startup Hub", 150, "Startup Workshop", 1, 50000, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("19a214ae-ad94-4c2d-bc84-f38a190494c5"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("1c25fb0b-367d-4875-a1e5-2bb254ac367d"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("524b2d29-ec13-47bc-8758-c7c5a6720074"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("661b5b70-5079-45c9-814e-b7f4a48c94f7"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("c202e143-0117-4580-bdca-034c86415393"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("dc5a170e-49bd-4af5-8198-4398207e7861"));

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "admins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "is_deleted", "password", "updated_at" },
                values: new object[] { new Guid("afd1b808-9dc4-44e0-ae2b-572920b7e13c"), null, "admin@admin.com", false, "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "additional_info", "deleted_at", "description", "event_date", "is_deleted", "location", "max_participants", "name", "status", "ticket_price", "updated_at" },
                values: new object[,]
                {
                    { new Guid("17de3bc4-d6d1-45d6-8359-4e0662771989"), null, null, "Explore the latest tech innovations", new DateTime(2024, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, "Expo Center", 300, "Tech Expo", 1, 100000, null },
                    { new Guid("497da941-5daf-49c5-a9e7-f7effbf3b914"), null, null, "Join us for a music extravaganza", new DateTime(2024, 12, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), false, "City Park", 1000, "Music Festival", 0, 200000, null },
                    { new Guid("89dc0337-32fc-44ea-bb34-fc2e90fc070e"), null, null, "Relax and rejuvenate with yoga", new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), false, "Wellness Resort", 200, "Yoga Retreat", 0, 150000, null },
                    { new Guid("8a6cc8b4-839e-473f-ab24-f0a4fd9e6f39"), null, null, "A grand concert to enjoy the night", new DateTime(2024, 12, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), false, "Stadium A", 500, "Concert Night", 0, 100000, null },
                    { new Guid("ee660b6d-ca3d-489c-a1fd-025a80585802"), null, null, "A workshop to boost your startup skills", new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "Startup Hub", 150, "Startup Workshop", 1, 50000, null }
                });
        }
    }
}
