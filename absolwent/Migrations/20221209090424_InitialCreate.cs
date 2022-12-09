using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace absolwent.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graduate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GraduationYear = table.Column<int>(type: "int", nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graduate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionnaireFrequency = table.Column<int>(type: "int", nullable: false),
                    PasswordResetKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordResetDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GraduateId = table.Column<long>(type: "bigint", nullable: false),
                    FillingData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendingData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Graduate_GraduateId",
                        column: x => x.GraduateId,
                        principalTable: "Graduate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionareId = table.Column<long>(type: "bigint", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProffesionalActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Earnings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanySize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TownSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobSearchTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodOfEmployment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobSatisfaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Training = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Data_Questionnaire_QuestionareId",
                        column: x => x.QuestionareId,
                        principalTable: "Questionnaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Data_QuestionareId",
                table: "Data",
                column: "QuestionareId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_GraduateId",
                table: "Questionnaire",
                column: "GraduateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "Questionnaire");

            migrationBuilder.DropTable(
                name: "Graduate");
        }
    }
}
