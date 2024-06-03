using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hustex_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB_V8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 21, 27, 36, 213, DateTimeKind.Local).AddTicks(2100),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 28, 21, 14, 53, 633, DateTimeKind.Local).AddTicks(8507));

            migrationBuilder.CreateIndex(
                name: "IX_File_FileName",
                table: "File",
                column: "FileName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_File_FileName",
                table: "File");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 21, 14, 53, 633, DateTimeKind.Local).AddTicks(8507),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 28, 21, 27, 36, 213, DateTimeKind.Local).AddTicks(2100));
        }
    }
}
