using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomOccupancy.Persistence.Migrations
{
    public partial class ChangeCampusModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Rooms_RoomId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Faculties_FacultyId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomDisponents");

            migrationBuilder.DropColumn(
                name: "DisponentType",
                table: "Disponents");

            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "Disponents");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Rooms",
                newName: "DisponentId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_FacultyId",
                table: "Rooms",
                newName: "IX_Rooms_DisponentId");

            migrationBuilder.AddColumn<int>(
                name: "DisponentId",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Faculties",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Equipment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Disponents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DisponentId",
                table: "Lecturers",
                column: "DisponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_RoomId",
                table: "Faculties",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Rooms_RoomId",
                table: "Equipment",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Rooms_RoomId",
                table: "Faculties",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Disponents_DisponentId",
                table: "Lecturers",
                column: "DisponentId",
                principalTable: "Disponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Disponents_DisponentId",
                table: "Rooms",
                column: "DisponentId",
                principalTable: "Disponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Rooms_RoomId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Rooms_RoomId",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Disponents_DisponentId",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Disponents_DisponentId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_DisponentId",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_RoomId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "DisponentId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Disponents");

            migrationBuilder.RenameColumn(
                name: "DisponentId",
                table: "Rooms",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_DisponentId",
                table: "Rooms",
                newName: "IX_Rooms_FacultyId");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisponentType",
                table: "Disponents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "Disponents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomDisponents",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    DisponentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDisponents", x => new { x.RoomId, x.DisponentId });
                    table.ForeignKey(
                        name: "FK_RoomDisponents_Disponents_DisponentId",
                        column: x => x.DisponentId,
                        principalTable: "Disponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomDisponents_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomDisponents_DisponentId",
                table: "RoomDisponents",
                column: "DisponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Rooms_RoomId",
                table: "Equipment",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Faculties_FacultyId",
                table: "Rooms",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
