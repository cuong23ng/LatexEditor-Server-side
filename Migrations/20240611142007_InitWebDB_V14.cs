using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hustex_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB_V14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 11, 21, 20, 4, 295, DateTimeKind.Local).AddTicks(453),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 6, 11, 20, 17, 25, 689, DateTimeKind.Local).AddTicks(2771));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 11, 20, 17, 25, 689, DateTimeKind.Local).AddTicks(2771),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 6, 11, 21, 20, 4, 295, DateTimeKind.Local).AddTicks(453));
        }
    }
}
