using System;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionTienda.Controllers
{
    [ApiController]
    [Route("/Compra")]
	public class CompraController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;
		public CompraController(AplicationDbContext context)
        {
           this.dbContext = context;
        // this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Compra>>> Get()
        {
            return await dbContext.Compra.Include(x=> x.carritos).ToListAsync();
        }

		[HttpPost]
		public async Task<ActionResult> Post(Compra compra)
		{
		    dbContext.Add(compra);
		    await dbContext.SaveChangesAsync();
		    return Ok();
		}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(Compra compra, int id)
        {
            if (compra.id_compra != id)
            {
                return BadRequest("El id de la compra no coincide con el establecido en la url");
            }

            dbContext.Update(compra);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Compra.AnyAsync(x => x.id_compra == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Compra()
            {
                id_compra = id
            });
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

