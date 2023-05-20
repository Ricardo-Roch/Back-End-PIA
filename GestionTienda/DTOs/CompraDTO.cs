using System;
using GestionTienda.Entidades;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.DTOs
{
	public class compraDTO
	{
        [Key]
        public int id_compra { get; set; }
        public int costo { get; set; }
        public int met_pago { get; set; }
        public String Direccion_env { get; set; }
        public List<Carrito> carritos { get; set; }
    }
}

