using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionTienda.Entidades
{
    public class Productos
    {
        [Key]
        public int id_producto { get; set; }
        public int Id_carrito { get; set; }
        public bool disponibilidad { get; set; }
        public string categoria { get; set; }
        public string Nombre_producto { get; set; }
        public string Imagen { get; set; }
    }
}

