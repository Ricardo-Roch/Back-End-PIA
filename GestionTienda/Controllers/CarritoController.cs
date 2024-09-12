using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionTienda.Entidades;


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
		public async Task<ActionResult<List<Carrito>>> GetAll()
		{
			return await dbContext.Carrito.ToListAsync();


		}
        /*[HttpPost]
		public async Task<ActionResult> Post(Carrito carrito)
		{
			dbContext.Add(carrito);
			await dbContext.SaveChangesAsync();
			return Ok();
		}*/

        [HttpPost]
        public async Task<ActionResult> Post(Carrito carrito)
        {
            dbContext.Add(carrito);
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

