using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hustex_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB_V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "File");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "File");

            migrationBuilder.DropColumn(
                name: "Data_type",
                table: "File");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 21, 14, 53, 633, DateTimeKind.Local).AddTicks(8507),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 27, 23, 58, 30, 534, DateTimeKind.Local).AddTicks(2737));

            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "File",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "File",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataType",
                table: "File");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "File");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 23, 58, 30, 534, DateTimeKind.Local).AddTicks(2737),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 28, 21, 14, 53, 633, DateTimeKind.Local).AddTicks(8507));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_type",
                table: "File",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }
    }
}
