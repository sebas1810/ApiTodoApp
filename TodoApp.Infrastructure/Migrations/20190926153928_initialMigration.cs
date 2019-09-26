using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApp.Infrastructure.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TODO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TODO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TODO_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Pendiente" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Resuelta" });

            migrationBuilder.InsertData(
                table: "TODO",
                columns: new[] { "Id", "Description", "FileUrl", "StatusId" },
                values: new object[] { 1, "Tarea uno", null, 1 });

            migrationBuilder.InsertData(
                table: "TODO",
                columns: new[] { "Id", "Description", "FileUrl", "StatusId" },
                values: new object[] { 2, "Tarea dos", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TODO_StatusId",
                table: "TODO",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TODO");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
