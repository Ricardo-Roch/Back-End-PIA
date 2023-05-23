using System;
using GestionTienda.Entidades;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.DTOs
{
	public class GetProducto
	{
        
        public int id_producto { get; set; }
        public bool disponibilidad { get; set; }
        public string categoria { get; set; }
        public string Nombre_producto { get; set; }
        public IFormFile Imagen { get; set; }

        public List<Carrito> carritos { get; set; }
    }
}

