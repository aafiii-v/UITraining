using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UITraining.Migrations
{
    /// <inheritdoc />
    public partial class Addsaltandpasswordhash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserAccesses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "UserAccesses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserAccesses");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "UserAccesses");
        }
    }
}
