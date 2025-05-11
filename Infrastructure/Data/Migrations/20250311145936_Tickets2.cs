using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FobumCinema.Migrations
{
    public partial class Tickets2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaHallId",
                table: "Screening",
                type: "int",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Screening_CinemaHallId",
                table: "Screening",
                column: "CinemaHallId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Screening_CinemaHall_CinemaHallId",
                table: "Screening",
                column: "CinemaHallId",
                principalTable: "CinemaHall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screening_CinemaHall_CinemaHallId",
                table: "Screening"
            );

            migrationBuilder.DropIndex(
                name: "IX_Screening_CinemaHallId",
                table: "Screening"
            );

            migrationBuilder.DropColumn(
                name: "CinemaHallId",
                table: "Screening"
            );
        }

    }
}
