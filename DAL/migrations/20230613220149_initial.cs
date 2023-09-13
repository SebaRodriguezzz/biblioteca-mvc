using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca_MVC.DAL.migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biblioteca",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaldoTotal = table.Column<double>(type: "float", nullable: false),
                    VentasRealizadas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    PrecioCompra = table.Column<double>(type: "float", nullable: false),
                    PrecioAlquiler = table.Column<double>(type: "float", nullable: false),
                    ISBN = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantPaginas = table.Column<int>(type: "int", nullable: false),
                    Alquilado = table.Column<bool>(type: "bit", nullable: false),
                    FechaDevolucionEnDias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biblioteca");

            migrationBuilder.DropTable(
                name: "Libros");
        }
    }
}
