using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RezervasyonApp.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Mekanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mekanlar_KullaniciId",
                table: "Mekanlar",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mekanlar_Kullanicilar_KullaniciId",
                table: "Mekanlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mekanlar_Kullanicilar_KullaniciId",
                table: "Mekanlar");

            migrationBuilder.DropIndex(
                name: "IX_Mekanlar_KullaniciId",
                table: "Mekanlar");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Mekanlar");
        }
    }
}
