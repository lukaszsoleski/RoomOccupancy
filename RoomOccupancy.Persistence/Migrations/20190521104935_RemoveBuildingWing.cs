using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class RemoveBuildingWing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_BuildingWings_BuildingWingId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "BuildingWings");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BuildingWingId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BuildingWindId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BuildingWingId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomDisponents");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reservations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "BuildingWindId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingWingId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomDisponents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BuildingWings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingWings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingWingId",
                table: "Rooms",
                column: "BuildingWingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_BuildingWings_BuildingWingId",
                table: "Rooms",
                column: "BuildingWingId",
                principalTable: "BuildingWings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
