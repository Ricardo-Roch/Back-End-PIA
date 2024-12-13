using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTienda.Migrations
{
    /// <inheritdoc />
    public partial class LaMigra2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Compra_Compraid_compra",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "CarritoCompra");

            migrationBuilder.DropTable(
                name: "CarritoProductos");

            migrationBuilder.DropTable(
                name: "CarritoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Compraid_compra",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Compraid_compra",
                table: "Usuario");

            migrationBuilder.AddColumn<int>(
                name: "CarritoId_carrito",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_carrito",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuarioid_usuario",
                table: "Compra",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_usuario",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Compraid_compra",
                table: "Carrito",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Usuarioid_usuario",
                table: "Carrito",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_compra",
                table: "Carrito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_usuario",
                table: "Carrito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CarritoId_carrito",
                table: "Productos",
                column: "CarritoId_carrito");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Usuarioid_usuario",
                table: "Compra",
                column: "Usuarioid_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_Compraid_compra",
                table: "Carrito",
                column: "Compraid_compra");

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_Usuarioid_usuario",
                table: "Carrito",
                column: "Usuarioid_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Compra_Compraid_compra",
                table: "Carrito",
                column: "Compraid_compra",
                principalTable: "Compra",
                principalColumn: "id_compra");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Usuario_Usuarioid_usuario",
                table: "Carrito",
                column: "Usuarioid_usuario",
                principalTable: "Usuario",
                principalColumn: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Usuario_Usuarioid_usuario",
                table: "Compra",
                column: "Usuarioid_usuario",
                principalTable: "Usuario",
                principalColumn: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Carrito_CarritoId_carrito",
                table: "Productos",
                column: "CarritoId_carrito",
                principalTable: "Carrito",
                principalColumn: "Id_carrito");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Compra_Compraid_compra",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Usuario_Usuarioid_usuario",
                table: "Carrito");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Usuario_Usuarioid_usuario",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Carrito_CarritoId_carrito",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CarritoId_carrito",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Compra_Usuarioid_usuario",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_Compraid_compra",
                table: "Carrito");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_Usuarioid_usuario",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "CarritoId_carrito",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Id_carrito",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Usuarioid_usuario",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "id_usuario",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "Compraid_compra",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "Usuarioid_usuario",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "id_compra",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "id_usuario",
                table: "Carrito");

            migrationBuilder.AddColumn<int>(
                name: "Compraid_compra",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarritoCompra",
                columns: table => new
                {
                    carritosId_carrito = table.Column<int>(type: "int", nullable: false),
                    comprasid_compra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoCompra", x => new { x.carritosId_carrito, x.comprasid_compra });
                    table.ForeignKey(
                        name: "FK_CarritoCompra_Carrito_carritosId_carrito",
                        column: x => x.carritosId_carrito,
                        principalTable: "Carrito",
                        principalColumn: "Id_carrito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoCompra_Compra_comprasid_compra",
                        column: x => x.comprasid_compra,
                        principalTable: "Compra",
                        principalColumn: "id_compra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarritoProductos",
                columns: table => new
                {
                    carritosId_carrito = table.Column<int>(type: "int", nullable: false),
                    productosid_producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoProductos", x => new { x.carritosId_carrito, x.productosid_producto });
                    table.ForeignKey(
                        name: "FK_CarritoProductos_Carrito_carritosId_carrito",
                        column: x => x.carritosId_carrito,
                        principalTable: "Carrito",
                        principalColumn: "Id_carrito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoProductos_Productos_productosid_producto",
                        column: x => x.productosid_producto,
                        principalTable: "Productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarritoUsuario",
                columns: table => new
                {
                    carritosId_carrito = table.Column<int>(type: "int", nullable: false),
                    usuariosid_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoUsuario", x => new { x.carritosId_carrito, x.usuariosid_usuario });
                    table.ForeignKey(
                        name: "FK_CarritoUsuario_Carrito_carritosId_carrito",
                        column: x => x.carritosId_carrito,
                        principalTable: "Carrito",
                        principalColumn: "Id_carrito",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoUsuario_Usuario_usuariosid_usuario",
                        column: x => x.usuariosid_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Compraid_compra",
                table: "Usuario",
                column: "Compraid_compra");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompra_comprasid_compra",
                table: "CarritoCompra",
                column: "comprasid_compra");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProductos_productosid_producto",
                table: "CarritoProductos",
                column: "productosid_producto");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoUsuario_usuariosid_usuario",
                table: "CarritoUsuario",
                column: "usuariosid_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Compra_Compraid_compra",
                table: "Usuario",
                column: "Compraid_compra",
                principalTable: "Compra",
                principalColumn: "id_compra");
        }
    }
}
