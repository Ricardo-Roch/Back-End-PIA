using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTienda.Entidades
{
	public class Usuario
	{
        [Key]
        public int id_usuario { get; set; }
        public String nombre { get; set; }
        public String correo { get; set; }
        public String contra { get; set; }
        [ForeignKey("id_usuario")]
        public List<Compra> compras{ get; set; }
        [ForeignKey("id_usuario")]
        public List<Carrito> carritos{ get; set; }

    }
}

