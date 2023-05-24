using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionTienda.Entidades;
using AutoMapper;
using GestionTienda.DTOs;
using GestionTienda.Mapping;

namespace GestionTienda.Controllers
{
    [ApiController]
	[Route("/Carrito")]
	public class CarritoController: ControllerBase
	{
		private readonly AplicationDbContext dbContext;
		public CarritoController(AplicationDbContext context)
		{
			this.dbContext = context;
			//this.configuration = configuration;
		}

        [HttpGet]
        public async Task<List<carritoDTO>> Get()
        {
            Automapper.Configure();
            var carr = await dbContext.Carrito.ToListAsync();

            return Mapper.Map<List<carritoDTO>>(carr);
        }




        [HttpPost("Carrito")]
        public async Task<ActionResult> Post(carritoDTO CarritoDTO)
        {
            Automapper.Configure();
            var carr = Mapper.Map<Carrito>(CarritoDTO);
            await dbContext.Carrito.AddAsync(carr);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
     

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(Carrito carrito, int id)
        {
            if (carrito.Id_carrito != id)
            {
                return BadRequest("El id del carrito no coincide con el establecido en la url");
            }

            dbContext.Update(carrito);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Carrito.AnyAsync(x => x.Id_carrito == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Carrito()
            {
                Id_carrito = id
            });
            await dbContext.SaveChangesAsync();

            return Ok();
        }


    }
}

