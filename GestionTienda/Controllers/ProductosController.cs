using System;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionTienda.Controllers
{
    [ApiController]
    [Route("/Productos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class ProductosController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;
        public ProductosController(AplicationDbContext context)
        {
            this.dbContext = context;
            // this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Productos>>> Get()
        {
            return await dbContext.Productos.Include(x => x.carritos).ToListAsync();

        }
        [HttpGet ("productos/{categoria}")]
        public IActionResult GetProductosPorCategoria(string categoria)
          {
            List<Productos> productos= dbContext.Productos.Where(p=> p.categoria==categoria).ToList();
            if (productos.Count == 0)
                {
                    return NotFound();
                }
             return Ok(productos);
          }
        
        [HttpGet("productos/nombre/{nombre}")]
        public IActionResult GetProductoPorNombre(string nombre)
        {
            // Aquí se debe realizar la lógica para buscar el producto por su nombre
            // Puedes utilizar una base de datos o cualquier otra fuente de datos

            // Ejemplo de búsqueda de producto por nombre
            Productos producto =dbContext.Productos.FirstOrDefault(p => p.Nombre_producto == nombre);

            if (producto == null)
            {
                return NotFound(); // Devuelve un error 404 si el producto no se encontró
            }

            return Ok(producto); // Devuelve el producto encontrado
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

