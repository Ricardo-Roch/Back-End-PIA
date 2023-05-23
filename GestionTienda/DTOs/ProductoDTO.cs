using System;
using GestionTienda.Entidades;

namespace GestionTienda.DTOs
{
	public class productoDTO
	{
        public int id_producto { get; set; }
        public bool disponibilidad { get; set; }
        public string categoria { get; set; }
        public string Imagen { get; set; }
        public string Nombre_producto { get; set; }
        //public List<Carrito> carritos { get; set; }
    }
}

