using System;
using GestionTienda.Entidades;

namespace GestionTienda.DTOs
{
	public class GetCompraDTO
	{
        public int id_compra { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public int costo { get; set; }
        public int met_pago { get; set; }
        public String Direccion_env { get; set; }
        public List<Carrito> carritos { get; set; }
    }
}

