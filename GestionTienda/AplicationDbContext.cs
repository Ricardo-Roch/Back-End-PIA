using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



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

            modelBuilder.Entity<Usuario>()
                .HasKey(al => new { al.id_usuario, al.carritos });

            /*
             * 
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>()
            .HasMany(c => c.Usuarios)
            .WithOne(u => u.Carrito)
            .HasForeignKey(u => u.CarritoId);
            }
*/
        }
        //LO DE LA BD
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuario> Usuario{ get; set; }
    }
}
