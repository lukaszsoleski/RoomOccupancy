using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class RemoveBuildingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Buildings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Buildings",
                nullable: true);
        }
    }
}
