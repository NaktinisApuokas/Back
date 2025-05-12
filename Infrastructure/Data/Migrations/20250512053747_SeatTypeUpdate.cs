using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FobumCinema.Migrations
{
    public partial class SeatTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatType_CinemaCompany_CinemaCompanyId",
                table: "SeatType");

            migrationBuilder.DropIndex(
                name: "IX_SeatType_CinemaCompanyId",
                table: "SeatType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SeatType_CinemaCompanyId",
                table: "SeatType",
                column: "CinemaCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatType_CinemaCompany_CinemaCompanyId",
                table: "SeatType",
                column: "CinemaCompanyId",
                principalTable: "CinemaCompany",
                principalColumn: "CinemaCompanyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
