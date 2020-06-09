using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class inter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Employee_EmployeeId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Employer_EmployerId",
                table: "Interest");

            migrationBuilder.AlterColumn<int>(
                name: "EmployerId",
                table: "Interest",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Interest",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Employee_EmployeeId",
                table: "Interest",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Employer_EmployerId",
                table: "Interest",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Employee_EmployeeId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Employer_EmployerId",
                table: "Interest");

            migrationBuilder.AlterColumn<int>(
                name: "EmployerId",
                table: "Interest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Interest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Employee_EmployeeId",
                table: "Interest",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Employer_EmployerId",
                table: "Interest",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
