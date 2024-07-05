using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hustex_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB_V10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_FileName",
                table: "File");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "File");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 3, 22, 28, 22, 715, DateTimeKind.Local).AddTicks(2363),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 5, 28, 21, 49, 40, 574, DateTimeKind.Local).AddTicks(1554));

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                columns: new[] { "FileName", "ProjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 28, 21, 49, 40, 574, DateTimeKind.Local).AddTicks(1554),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 6, 3, 22, 28, 22, 715, DateTimeKind.Local).AddTicks(2363));

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "File",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_File_FileName",
                table: "File",
                column: "FileName",
                unique: true);
        }
    }
}
