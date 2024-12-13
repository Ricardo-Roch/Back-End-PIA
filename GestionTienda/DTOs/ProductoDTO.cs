using System;
using System.ComponentModel.DataAnnotations;
using GestionTienda.Entidades;

namespace GestionTienda.DTOs
{
	public class productoDTO
	{
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo debe ser un número entero.")]
        public int id_producto { get; set; }
        [Required(ErrorMessage = "El campo Disponible es obligatorio.")]
        public bool disponibilidad { get; set; }
        public string categoria { get; set; }
        public string Imagen { get; set; }
        public string Nombre_producto { get; set; }
        //public List<Carrito> carritos { get; set; }
    }
}

