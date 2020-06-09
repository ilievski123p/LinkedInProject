using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class Sredeno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "OglasId",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "OglasId",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "OglasId",
                table: "Oglas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "Interest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OglasId",
                table: "Oglas");

            migrationBuilder.DropColumn(
                name: "InterestId",
                table: "Interest");

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "Employer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OglasId",
                table: "Employer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InterestId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OglasId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
