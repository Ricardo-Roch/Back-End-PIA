using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionTienda.DTOs;

namespace GestionTienda.Entidades
{
	public class Compra
	{
        [Key]
        public int id_compra { get; set; }
        public int id_usuario { get; set; }
        public int costo { get; set; }
        public int met_pago { get; set; }
        public String Direccion_env { get; set; }
        [ForeignKey("id_compra")]
        public List<Carrito> ca { get; set; }


    }
}

