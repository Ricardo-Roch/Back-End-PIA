using System;
using GestionTienda.Entidades;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.DTOs
{
	public class GetProducto
	{

        [Key]
        public int id_producto { get; set; }
        public int Id_carrito { get; set; }
        public bool disponibilidad { get; set; }
        public string categoria { get; set; }
        public string Nombre_producto { get; set; }
        public IFormFile Imagen { get; set; }

        
    }
}

