using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1WEBMVC.Migrations
{
    /// <inheritdoc />
    public partial class Mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Payments",
                newName: "ConsultationId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "ConsultationId",
                table: "Payments",
                newName: "AppointmentId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
