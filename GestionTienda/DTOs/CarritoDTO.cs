using System;
using System.ComponentModel.DataAnnotations;
using GestionTienda.Entidades;
using GestionTienda.Validaciones;

namespace GestionTienda.DTOs
{
	public class carritoDTO
	{

        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [PrimeraLetraMayuscula]

        public int Id_carrito { get; set; }
        public int id_usuario { get; set; }
        public int costo_total { get; set; }
        //public int id_compra { get; set; }
        //public List<Productos> productos { get; set; }

    }
}

