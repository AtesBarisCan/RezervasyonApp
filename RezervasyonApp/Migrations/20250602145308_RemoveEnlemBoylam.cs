using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RezervasyonApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEnlemBoylam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GorselUrl",
                table: "Mekanlar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GorselUrl",
                table: "Mekanlar");
        }
    }
}
