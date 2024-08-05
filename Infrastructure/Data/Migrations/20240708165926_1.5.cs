using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FobumCinema.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTimeUtc",
                table: "Cinema");

            migrationBuilder.AddColumn<int>(
                name: "CinemaCompanyID",
                table: "Cinema",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CinemaCompany",
                columns: table => new
                {
                    CinemaCompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaCompany", x => x.CinemaCompanyID);
                });

            migrationBuilder.CreateTable(
                name: "HallType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HallType_CinemaCompany_CinemaCompanyId",
                        column: x => x.CinemaCompanyId,
                        principalTable: "CinemaCompany",
                        principalColumn: "CinemaCompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CinemaHall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    HasDisabledSeats = table.Column<bool>(type: "bit", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false),
                    HallTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaHall_Cinema_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinemaHall_HallType_HallTypeId",
                        column: x => x.HallTypeId,
                        principalTable: "HallType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHall_CinemaId",
                table: "CinemaHall",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHall_HallTypeId",
                table: "CinemaHall",
                column: "HallTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HallType_CinemaCompanyId",
                table: "HallType",
                column: "CinemaCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaHall");

            migrationBuilder.DropTable(
                name: "HallType");

            migrationBuilder.DropTable(
                name: "CinemaCompany");

            migrationBuilder.DropColumn(
                name: "CinemaCompanyID",
                table: "Cinema");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTimeUtc",
                table: "Cinema",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
