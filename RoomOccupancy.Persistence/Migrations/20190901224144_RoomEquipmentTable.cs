using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class RoomEquipmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Rooms_RoomId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_RoomId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Equipment");

            migrationBuilder.CreateTable(
                name: "RoomEquipment",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEquipment", x => new { x.RoomId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_RoomEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomEquipment_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomEquipment_EquipmentId",
                table: "RoomEquipment",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomEquipment");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Equipment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RoomId",
                table: "Equipment",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Rooms_RoomId",
                table: "Equipment",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
