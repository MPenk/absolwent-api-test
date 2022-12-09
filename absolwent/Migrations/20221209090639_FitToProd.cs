using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace absolwent.Migrations
{
    public partial class FitToProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaire_Graduate_GraduateId",
                table: "Questionnaire");

            migrationBuilder.RenameColumn(
                name: "GraduateId",
                table: "Questionnaire",
                newName: "Graduate_id");

            migrationBuilder.RenameIndex(
                name: "IX_Questionnaire_GraduateId",
                table: "Questionnaire",
                newName: "IX_Questionnaire_Graduate_id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Graduate",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "GraduationYear",
                table: "Graduate",
                newName: "Graduation_year");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Graduate",
                newName: "Graduate_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaire_Graduate_Graduate_id",
                table: "Questionnaire",
                column: "Graduate_id",
                principalTable: "Graduate",
                principalColumn: "Graduate_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaire_Graduate_Graduate_id",
                table: "Questionnaire");

            migrationBuilder.RenameColumn(
                name: "Graduate_id",
                table: "Questionnaire",
                newName: "GraduateId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionnaire_Graduate_id",
                table: "Questionnaire",
                newName: "IX_Questionnaire_GraduateId");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Graduate",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Graduation_year",
                table: "Graduate",
                newName: "GraduationYear");

            migrationBuilder.RenameColumn(
                name: "Graduate_id",
                table: "Graduate",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaire_Graduate_GraduateId",
                table: "Questionnaire",
                column: "GraduateId",
                principalTable: "Graduate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
