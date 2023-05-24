using System;
using GestionTienda.Entidades;

namespace GestionTienda.DTOs
{
	public class CompraUsuarioDto
	{
        public int id_compra { get; set; }
        public Usuario usuario { get; set; }
        public int Id_usuario { get; set; }
        public Compra compra { get; set; }
    }
}

