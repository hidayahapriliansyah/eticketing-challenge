using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETicketing.Migrations
{
    /// <inheritdoc />
    public partial class SeederEventData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("d5798d80-0d65-4f44-b389-9f1e65ec43f7"));

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "is_deleted", "password", "updated_at" },
                values: new object[] { new Guid("6165f591-7eec-4b25-9743-c82c57f47a45"), null, "admin@admin.com", false, "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "additional_info", "deleted_at", "description", "event_date", "is_deleted", "location", "max_participants", "name", "status", "ticket_price", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4a99648d-16ce-47af-9be6-c30c84eae063"), null, null, "A grand concert to enjoy the night", new DateTime(2024, 12, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), false, "Stadium A", 500, "Concert Night", 0, 100000, null },
                    { new Guid("56071ee0-1f8d-440b-a028-2654715edfd1"), null, null, "A workshop to boost your startup skills", new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "Startup Hub", 150, "Startup Workshop", 1, 50000, null },
                    { new Guid("69bec892-e1d2-464b-b67f-756aaa9c7ba1"), null, null, "Join us for a music extravaganza", new DateTime(2024, 12, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), false, "City Park", 1000, "Music Festival", 0, 200000, null },
                    { new Guid("93d6cdb2-bb94-4b26-8446-0f24b9000a90"), null, null, "Explore the latest tech innovations", new DateTime(2024, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), false, "Expo Center", 300, "Tech Expo", 1, 100000, null },
                    { new Guid("f35fcc35-8f1a-4c61-ab9d-5e5bf616c0da"), null, null, "Relax and rejuvenate with yoga", new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), false, "Wellness Resort", 200, "Yoga Retreat", 0, 150000, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("6165f591-7eec-4b25-9743-c82c57f47a45"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("4a99648d-16ce-47af-9be6-c30c84eae063"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("56071ee0-1f8d-440b-a028-2654715edfd1"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("69bec892-e1d2-464b-b67f-756aaa9c7ba1"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("93d6cdb2-bb94-4b26-8446-0f24b9000a90"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("f35fcc35-8f1a-4c61-ab9d-5e5bf616c0da"));

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "is_deleted", "password", "updated_at" },
                values: new object[] { new Guid("d5798d80-0d65-4f44-b389-9f1e65ec43f7"), null, "admin@admin.com", false, "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });
        }
    }
}
