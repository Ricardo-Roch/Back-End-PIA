using System;
using System.ComponentModel.DataAnnotations;
using GestionTienda.Entidades;

namespace GestionTienda.DTOs
{
	public class UsuarioDTO
	{
        public int id_usuario { get; set; }
        public String nombre { get; set; }
        public String correo { get; set; }
        public String contra { get; set; }
        //public List<Carrito> carritos { get; set; }
    }
}

