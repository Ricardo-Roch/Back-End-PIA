using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTienda.Migrations
{
    /// <inheritdoc />
    public partial class LaMigra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Compraid_compra",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompraCarrito",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false),
                    Id_carrito = table.Column<int>(type: "int", nullable: false),
                    CarritoId_carrito = table.Column<int>(type: "int", nullable: true),
                    compraid_compra = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraCarrito", x => new { x.id_compra, x.Id_carrito });
                    table.ForeignKey(
                        name: "FK_CompraCarrito_Carrito_CarritoId_carrito",
                        column: x => x.CarritoId_carrito,
                        principalTable: "Carrito",
                        principalColumn: "Id_carrito");
                    table.ForeignKey(
                        name: "FK_CompraCarrito_Compra_compraid_compra",
                        column: x => x.compraid_compra,
                        principalTable: "Compra",
                        principalColumn: "id_compra");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Compraid_compra",
                table: "Usuario",
                column: "Compraid_compra");

            migrationBuilder.CreateIndex(
                name: "IX_CompraCarrito_CarritoId_carrito",
                table: "CompraCarrito",
                column: "CarritoId_carrito");

            migrationBuilder.CreateIndex(
                name: "IX_CompraCarrito_compraid_compra",
                table: "CompraCarrito",
                column: "compraid_compra");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Compra_Compraid_compra",
                table: "Usuario",
                column: "Compraid_compra",
                principalTable: "Compra",
                principalColumn: "id_compra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Compra_Compraid_compra",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "CompraCarrito");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Compraid_compra",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Compraid_compra",
                table: "Usuario");
        }
    }
}
