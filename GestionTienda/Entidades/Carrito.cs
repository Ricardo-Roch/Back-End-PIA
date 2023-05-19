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
        public int costo_total { get; set; }
        public List<Productos>? productos { get; set; }
        public List<Compra>? compras { get; set; }
        [NotMapped]
        public List<Usuario>? usuarios { get; set; }

    }
}

