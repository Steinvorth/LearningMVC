using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectoCursoWeb_BlogCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameNombreToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Category",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Category",
                newName: "Nombre");
        }
    }
}
