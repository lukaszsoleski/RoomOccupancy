using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class RemoveMapPositionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_CampusMapPosition_CampusMapPositionId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "CampusMapPosition");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_CampusMapPositionId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "CampusMapPositionId",
                table: "Buildings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampusMapPositionId",
                table: "Buildings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CampusMapPosition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    XPos = table.Column<int>(nullable: false),
                    YPos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampusMapPosition", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CampusMapPositionId",
                table: "Buildings",
                column: "CampusMapPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_CampusMapPosition_CampusMapPositionId",
                table: "Buildings",
                column: "CampusMapPositionId",
                principalTable: "CampusMapPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
