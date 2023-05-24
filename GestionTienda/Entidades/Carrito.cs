using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace GestionTienda.Entidades
{
	public class Carrito
	{
        [Key]
        public int Id_carrito { get; set; }
        public int id_usuario { get; set; }
        public int costo_total { get; set; }
        public int id_compra { get; set; }
        [ForeignKey("Id_carrito")] 
        public List<Productos> productos { get; set; }

    }
}

