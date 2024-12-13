using System;
using GestionTienda.Validaciones;
using System.ComponentModel.DataAnnotations;
using GestionTienda.Entidades;

namespace GestionTienda.DTOs
{
	public class GetCarritoDTO
	{

        public int Id_carrito { get; set; }
        public int id_usuario { get; set; }
        public int costo_total { get; set; }
        public int id_compra { get; set; }
        public List<Productos> productos { get; set; }

    }
}

