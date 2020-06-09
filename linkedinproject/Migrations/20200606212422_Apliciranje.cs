using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class Apliciranje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apliciraj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: true),
                    AplicirajId = table.Column<int>(nullable: true),
                    OglasId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apliciraj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apliciraj_Employee_AplicirajId",
                        column: x => x.AplicirajId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apliciraj_Oglas_AplicirajId",
                        column: x => x.AplicirajId,
                        principalTable: "Oglas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apliciraj_AplicirajId",
                table: "Apliciraj",
                column: "AplicirajId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apliciraj");
        }
    }
}
