using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1WEBMVC.Migrations
{
    /// <inheritdoc />
    public partial class MG3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Patients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
