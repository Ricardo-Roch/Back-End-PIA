using System;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.Entidades
{
	public class CompraCarrito
	{
        public int id_compra { get; set; }
        public Carrito Carrito { get; set; }
        public int Id_carrito { get; set; }
        public Compra compra { get; set; }
    }
}

