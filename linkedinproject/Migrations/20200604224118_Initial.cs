using Microsoft.EntityFrameworkCore.Migrations;

namespace linkedinproject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    ProfilePicutre = table.Column<string>(nullable: true),
                    CoverPhoto = table.Column<string>(nullable: true),
                    CV = table.Column<string>(nullable: true),
                    CoverLetter = table.Column<string>(nullable: true),
                    GitHubLink = table.Column<string>(nullable: true),
                    CurrentPosition = table.Column<string>(nullable: true),
                    WantedPosition = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    OglasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CEOName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    OglasId = table.Column<int>(nullable: false),
                    InterestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployerId = table.Column<int>(nullable: false),
                    InterestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interest_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interest_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oglas_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oglas_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interest_EmployeeId",
                table: "Interest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_EmployerId",
                table: "Interest",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_EmployeeId",
                table: "Oglas",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_EmployerId",
                table: "Oglas",
                column: "EmployerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Employer");
        }
    }
}
