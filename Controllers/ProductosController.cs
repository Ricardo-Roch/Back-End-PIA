using System;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionTienda.Controllers
{
    [ApiController]
    [Route("/Productos")]
	public class ProductosController : ControllerBase
    {
		private readonly AplicationDbContext dbContext;
		public  ProductosController(AplicationDbContext context)
        {
            this.dbContext = context;
          // this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Productos>>> Get()
        {
            return await dbContext.Productos.Include(x=> x.carritos).ToListAsync();
        }
		
		[HttpPost]
		public async Task<ActionResult> Post(Productos productos)
		{
		    dbContext.Add(productos);
		    await dbContext.SaveChangesAsync();
		    return Ok();
		}

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(Productos productos, int id)
        {
            if (productos.id_producto != id)
            {
                return BadRequest("El id del producto no coincide con el establecido en la url");
            }

            dbContext.Update(productos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Productos.AnyAsync(x => x.id_producto == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Productos()
            {
                id_producto = id
            });
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

