using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hustex_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB_V9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "File");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 21, 49, 40, 574, DateTimeKind.Local).AddTicks(1554),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 28, 21, 27, 36, 213, DateTimeKind.Local).AddTicks(2100));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 21, 27, 36, 213, DateTimeKind.Local).AddTicks(2100),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 28, 21, 49, 40, 574, DateTimeKind.Local).AddTicks(1554));

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "File",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
