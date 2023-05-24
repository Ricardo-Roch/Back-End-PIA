using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GestionTienda.DTOs;

namespace GestionTienda
{
    public class AplicationDbContext : IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompraCarrito>()
                .HasKey(al => new { al.id_compra, al.Id_carrito });
            

         
        }
        //LO DE LA BD
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuario> Usuario{ get; set; }
    }
}
