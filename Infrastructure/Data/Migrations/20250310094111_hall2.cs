using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FobumCinema.Migrations
{
    public partial class hall2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatType_CinemaHall_CinemaHallId",
                table: "SeatType");

            migrationBuilder.DropIndex(
                name: "IX_SeatType_CinemaHallId",
                table: "SeatType");

            migrationBuilder.DropColumn(
                name: "CinemaHallId",
                table: "SeatType");

            migrationBuilder.AddColumn<string>(
                name: "CellMatrixJson",
                table: "CinemaHall",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellMatrixJson",
                table: "CinemaHall");

            migrationBuilder.AddColumn<int>(
                name: "CinemaHallId",
                table: "SeatType",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatType_CinemaHallId",
                table: "SeatType",
                column: "CinemaHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatType_CinemaHall_CinemaHallId",
                table: "SeatType",
                column: "CinemaHallId",
                principalTable: "CinemaHall",
                principalColumn: "Id");
        }
    }
}
