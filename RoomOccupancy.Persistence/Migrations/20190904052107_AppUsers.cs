using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class AppUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "AspNetUsers");
        }
    }
}
