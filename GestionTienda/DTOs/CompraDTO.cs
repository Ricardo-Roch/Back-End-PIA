using System;
using GestionTienda.Entidades;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.DTOs
{
	public class compraDTO
	{
        [Key]
        public int id_compra { get; set; }
        public int id_usuario { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo debe ser un número entero.")]
        public int costo { get; set; }
        [Range(1, 2, ErrorMessage = "La cantidad debe ser 1 o 2.")]
        public int met_pago { get; set; }
        public String Direccion_env { get; set; }
        public List<Carrito> ca { get; set; }
    }
}

