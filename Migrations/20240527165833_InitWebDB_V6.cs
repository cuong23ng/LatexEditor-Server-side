using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hustex_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB_V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 23, 58, 30, 534, DateTimeKind.Local).AddTicks(2737));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Project");
        }
    }
}
