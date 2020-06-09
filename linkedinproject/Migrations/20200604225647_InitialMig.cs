using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class InitialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "Interest");

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "Employee",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "Interest",
                type: "int",
                nullable: true);
        }
    }
}
