using System;
using System.ComponentModel.DataAnnotations;
using GestionTienda.Entidades;
using GestionTienda.Validaciones;

namespace GestionTienda.DTOs
{
	public class carritoDTO
	{

        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        [PrimeraLetraMayuscula]
        [Key]
        public int Id_carrito { get; set; }
        public int costo_total { get; set; }
         //public List<Productos> productos { get; set; }
        //public List<Compra> compras { get; set; }
       //public List<Usuario> usuarios { get; set; }
    }
}

