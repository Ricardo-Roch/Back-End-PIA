using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTienda.Migrations
{
    /// <inheritdoc />
    public partial class IntitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrito",
                columns: table => new
                {
                    Id_carrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    costo_total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrito", x => x.Id_carrito);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    costo = table.Column<int>(type: "int", nullable: false),
                    met_pago = table.Column<int>(type: "int", nullable: false),
                    Direccion_env = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.id_compra);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disponibilidad = table.Column<bool>(type: "bit", nullable: false),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre_producto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id_producto);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<int>(type: "int", nullable: false),
                    correo = table.Column<int>(type: "int", nullable: false),
                    contra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoCompra");

            migrationBuilder.DropTable(
                name: "CarritoProductos");

            migrationBuilder.DropTable(
                name: "CarritoUsuario");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Carrito");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
