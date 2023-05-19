using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionTienda.Entidades
{
	public class Usuario
	{
        [Key]
        public int id_usuario { get; set; }
        public int nombre { get; set; }
        public int correo { get; set; }
        public int contra { get; set; }
        public List<Carrito> carritos { get; set; }
    }
}

