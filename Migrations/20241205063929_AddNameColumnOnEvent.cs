using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicketing.Migrations
{
    /// <inheritdoc />
    public partial class AddNameColumnOnEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("55cbfb86-676e-4e86-8247-93b6a222e1be"));

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "is_deleted", "password", "updated_at" },
                values: new object[] { new Guid("d5798d80-0d65-4f44-b389-9f1e65ec43f7"), null, "admin@admin.com", false, "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("d5798d80-0d65-4f44-b389-9f1e65ec43f7"));

            migrationBuilder.DropColumn(
                name: "name",
                table: "events");

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id", "deleted_at", "email", "is_deleted", "password", "updated_at" },
                values: new object[] { new Guid("55cbfb86-676e-4e86-8247-93b6a222e1be"), null, "admin@admin.com", false, "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });
        }
    }
}
