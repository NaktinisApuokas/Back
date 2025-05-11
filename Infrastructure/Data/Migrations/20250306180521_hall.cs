using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FobumCinema.Migrations
{
    public partial class hall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaHall_HallType_HallTypeId",
                table: "CinemaHall");

            migrationBuilder.DropIndex(
                name: "IX_CinemaHall_HallTypeId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHall_HallTypeId",
                table: "CinemaHall",
                column: "HallTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaHall_HallType_HallTypeId",
                table: "CinemaHall",
                column: "HallTypeId",
                principalTable: "HallType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
