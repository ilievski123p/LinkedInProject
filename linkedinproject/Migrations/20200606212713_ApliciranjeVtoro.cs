using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class ApliciranjeVtoro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apliciraj_Employee_AplicirajId",
                table: "Apliciraj");

            migrationBuilder.DropForeignKey(
                name: "FK_Apliciraj_Oglas_AplicirajId",
                table: "Apliciraj");

            migrationBuilder.DropIndex(
                name: "IX_Apliciraj_AplicirajId",
                table: "Apliciraj");

            migrationBuilder.CreateIndex(
                name: "IX_Apliciraj_EmployeeId",
                table: "Apliciraj",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Apliciraj_OglasId",
                table: "Apliciraj",
                column: "OglasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apliciraj_Employee_EmployeeId",
                table: "Apliciraj",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Apliciraj_Oglas_OglasId",
                table: "Apliciraj",
                column: "OglasId",
                principalTable: "Oglas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apliciraj_Employee_EmployeeId",
                table: "Apliciraj");

            migrationBuilder.DropForeignKey(
                name: "FK_Apliciraj_Oglas_OglasId",
                table: "Apliciraj");

            migrationBuilder.DropIndex(
                name: "IX_Apliciraj_EmployeeId",
                table: "Apliciraj");

            migrationBuilder.DropIndex(
                name: "IX_Apliciraj_OglasId",
                table: "Apliciraj");

            migrationBuilder.CreateIndex(
                name: "IX_Apliciraj_AplicirajId",
                table: "Apliciraj",
                column: "AplicirajId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apliciraj_Employee_AplicirajId",
                table: "Apliciraj",
                column: "AplicirajId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Apliciraj_Oglas_AplicirajId",
                table: "Apliciraj",
                column: "AplicirajId",
                principalTable: "Oglas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
