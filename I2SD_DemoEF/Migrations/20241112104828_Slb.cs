using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I2SD_DemoEF.Migrations
{
    /// <inheritdoc />
    public partial class Slb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlbID",
                table: "Students",
                type: "char(5)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SlbID",
                table: "Students",
                column: "SlbID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teachers_SlbID",
                table: "Students",
                column: "SlbID",
                principalTable: "Teachers",
                principalColumn: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teachers_SlbID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SlbID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SlbID",
                table: "Students");
        }
    }
}
