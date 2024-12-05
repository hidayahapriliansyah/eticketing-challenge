using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicketing.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedAndSeederAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("55cbfb86-676e-4e86-8247-93b6a222e1be"), null, "admin@admin.com", false, "8cb671ed74c9c851fee146b8e0c3d951feb9dcccbdd92203316d6259c77c2744", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id",
                keyValue: new Guid("55cbfb86-676e-4e86-8247-93b6a222e1be"));

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
        }
    }
}
