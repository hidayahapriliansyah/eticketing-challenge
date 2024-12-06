using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETicketing.Migrations
{
    /// <inheritdoc />
    public partial class SeederCustomerData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "password", "updated_at" },
                values: new object[] { new Guid("eeb1e7d9-8928-40d3-a4e9-dc50c4e27508"), null, "admin@admin.com", "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id", "deleted_at", "email", "name", "password", "updated_at", "username" },
                values: new object[] { new Guid("84c308f3-4710-49c3-8d6a-0da1a936058d"), null, "customer@customer.com", "John Doe", "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null, "cusomer" });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "additional_info", "deleted_at", "description", "event_date", "location", "max_participants", "name", "status", "ticket_price", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0814b305-8376-4b56-a6e7-3f6bd8b2ec24"), null, null, "A grand concert to enjoy the night", new DateTime(2024, 12, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), "Stadium A", 500, "Concert Night", 0, 100000, null },
                    { new Guid("425f6311-5d66-4dc5-a3f2-3d01fc74750f"), null, null, "Explore the latest tech innovations", new DateTime(2024, 11, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "Expo Center", 300, "Tech Expo", 1, 100000, null },
                    { new Guid("76153e54-cc9d-4a90-8659-538a32688a4b"), null, null, "A workshop to boost your startup skills", new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Startup Hub", 150, "Startup Workshop", 1, 50000, null },
                    { new Guid("93d6f2ae-4bd3-4c2c-af88-2b002713ec87"), null, null, "Relax and rejuvenate with yoga", new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "Wellness Resort", 200, "Yoga Retreat", 0, 150000, null },
                    { new Guid("c6222bdc-d123-4e34-98c9-242b63645fef"), null, null, "Join us for a music extravaganza", new DateTime(2024, 12, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), "City Park", 1000, "Music Festival", 0, 200000, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("eeb1e7d9-8928-40d3-a4e9-dc50c4e27508"));

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "id",
                keyValue: new Guid("84c308f3-4710-49c3-8d6a-0da1a936058d"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("0814b305-8376-4b56-a6e7-3f6bd8b2ec24"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("425f6311-5d66-4dc5-a3f2-3d01fc74750f"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("76153e54-cc9d-4a90-8659-538a32688a4b"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("93d6f2ae-4bd3-4c2c-af88-2b002713ec87"));

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: new Guid("c6222bdc-d123-4e34-98c9-242b63645fef"));

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
    }
}
