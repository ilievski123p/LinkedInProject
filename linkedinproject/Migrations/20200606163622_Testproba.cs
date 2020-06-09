using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class Testproba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oglas_Employee_EmployeeId",
                table: "Oglas");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Oglas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Oglas_Employee_EmployeeId",
                table: "Oglas",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oglas_Employee_EmployeeId",
                table: "Oglas");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Oglas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Oglas_Employee_EmployeeId",
                table: "Oglas",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
