using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuarderiaApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonasAutorizadasToNiño : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alergias",
                table: "Niños",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alergias",
                table: "Niños");
        }
    }
}
