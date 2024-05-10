using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FobumCinema.Migrations
{
    public partial class fixedScreenings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hall",
                table: "Screening");

            migrationBuilder.RenameColumn(
                name: "emptyseatnumber",
                table: "Screening",
                newName: "Emptyseatnumber");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Screening",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Screening",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emptyseatnumber",
                table: "Screening",
                newName: "emptyseatnumber");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Screening",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Screening",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Hall",
                table: "Screening",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
