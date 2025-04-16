using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuarderiaApp.Migrations
{
    /// <inheritdoc />
    public partial class TodoActualizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NiñoPersonaAutorizada");

            migrationBuilder.AddColumn<string>(
                name: "CuentaBancaria",
                table: "PersonasAutorizadas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EsResponsablePago",
                table: "PersonasAutorizadas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NiñoId",
                table: "PersonasAutorizadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonasAutorizadas_NiñoId",
                table: "PersonasAutorizadas",
                column: "NiñoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasAutorizadas_Niños_NiñoId",
                table: "PersonasAutorizadas",
                column: "NiñoId",
                principalTable: "Niños",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonasAutorizadas_Niños_NiñoId",
                table: "PersonasAutorizadas");

            migrationBuilder.DropIndex(
                name: "IX_PersonasAutorizadas_NiñoId",
                table: "PersonasAutorizadas");

            migrationBuilder.DropColumn(
                name: "CuentaBancaria",
                table: "PersonasAutorizadas");

            migrationBuilder.DropColumn(
                name: "EsResponsablePago",
                table: "PersonasAutorizadas");

            migrationBuilder.DropColumn(
                name: "NiñoId",
                table: "PersonasAutorizadas");

            migrationBuilder.AlterColumn<string>(
                name: "Alergias",
                table: "Niños",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "NiñoPersonaAutorizada",
                columns: table => new
                {
                    NiñosId = table.Column<int>(type: "int", nullable: false),
                    PersonasAutorizadasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiñoPersonaAutorizada", x => new { x.NiñosId, x.PersonasAutorizadasId });
                    table.ForeignKey(
                        name: "FK_NiñoPersonaAutorizada_Niños_NiñosId",
                        column: x => x.NiñosId,
                        principalTable: "Niños",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NiñoPersonaAutorizada_PersonasAutorizadas_PersonasAutorizadasId",
                        column: x => x.PersonasAutorizadasId,
                        principalTable: "PersonasAutorizadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NiñoPersonaAutorizada_PersonasAutorizadasId",
                table: "NiñoPersonaAutorizada",
                column: "PersonasAutorizadasId");
        }
    }
}
