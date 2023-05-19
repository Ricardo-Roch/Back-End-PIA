using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using GestionTienda.Entidades;


namespace GestionTienda
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        //LO DE LA BD
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuario> Usuario{ get; set; }
    }
}
