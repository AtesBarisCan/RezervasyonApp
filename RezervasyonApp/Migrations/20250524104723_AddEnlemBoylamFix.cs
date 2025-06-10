using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RezervasyonApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEnlemBoylamFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Boylam",
                table: "Mekanlar",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Enlem",
                table: "Mekanlar",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boylam",
                table: "Mekanlar");

            migrationBuilder.DropColumn(
                name: "Enlem",
                table: "Mekanlar");
        }
    }
}
