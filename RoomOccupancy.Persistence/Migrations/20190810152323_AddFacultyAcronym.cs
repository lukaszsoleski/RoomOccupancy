using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class AddFacultyAcronym : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Departments_DepartmentId",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Rooms_RoomId",
                table: "Faculties");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_DepartmentId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_RoomId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Faculties");

            migrationBuilder.AddColumn<string>(
                name: "Acronym",
                table: "Faculties",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacultyRooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyRooms", x => new { x.RoomId, x.FacultyId });
                    table.ForeignKey(
                        name: "FK_FacultyRooms_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyRooms_FacultyId",
                table: "FacultyRooms",
                column: "FacultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyRooms");

            migrationBuilder.DropColumn(
                name: "Acronym",
                table: "Faculties");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Faculties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Faculties",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_DepartmentId",
                table: "Faculties",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_RoomId",
                table: "Faculties",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Departments_DepartmentId",
                table: "Faculties",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Rooms_RoomId",
                table: "Faculties",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
