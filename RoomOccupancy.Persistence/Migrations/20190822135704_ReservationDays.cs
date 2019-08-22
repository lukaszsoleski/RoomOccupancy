using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class ReservationDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationDays",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDays",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Reservations");
        }
    }
}
