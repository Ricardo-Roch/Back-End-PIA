using System;
using AutoMapper;
using GestionTienda.DTOs;
using GestionTienda.Entidades;
using GestionTienda.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionTienda.Controllers
{
    [ApiController]
    [Route("/Productos")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class ProductosController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;
        public ProductosController(AplicationDbContext context)
        {
            this.dbContext = context;
            // this.configuration = configuration;
        }

        [HttpGet("Productos")]
        public async Task<List<productoDTO>> Get()
        {
            Automapper.Configure();
            var producto = await dbContext.Productos.ToListAsync();
            return Mapper.Map<List<productoDTO>>(producto);
        }

        


        [HttpGet("productos/{categoria}")]
        public IActionResult GetProductosPorCategoria(string categoria)
        {
            List<Productos> productos = dbContext.Productos.Where(p => p.categoria == categoria).ToList();
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
            Productos producto = dbContext.Productos.FirstOrDefault(p => p.Nombre_producto == nombre);

            if (producto == null)
            {
                return NotFound(); // Devuelve un error 404 si el producto no se encontró
            }

            return Ok(producto); // Devuelve el producto encontrado
        }
        /*
        [HttpPost("Prroductos")]
		public async Task<ActionResult> Post(productoDTO productos)
		{
            Automapper.Configure();
            var productos1 = Mapper.Map<Productos>(productos);
            await dbContext.Productos.AddAsync(productos1);
            await dbContext.SaveChangesAsync();
            return Ok();

		}*/
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromForm]GetProducto productoDTO)
        {
            //Automapper.Configure();
           // var productos1 = Mapper.Map<Productos>(productoDTO);
            string fotoUrl = string.Empty; // Valor predeterminado para fotoUrl

            if (productoDTO.Imagen != null)
            {
                // Procesar y guardar la imagen
                fotoUrl = await GuardarFoto(productoDTO.Imagen);
            }

            // Asignar la URL de la imagen a la propiedad Foto
            var producto = new Productos
            {
                id_producto = productoDTO.id_producto,
                disponibilidad = productoDTO.disponibilidad,
                categoria = productoDTO.categoria,
                Nombre_producto = productoDTO.Nombre_producto,
                Imagen = fotoUrl,
                carritos = productoDTO.carritos
            };

            // Guardar la entidad en la base de datos
            dbContext.Productos.Add(producto);
            await dbContext.SaveChangesAsync();
            //await dbContext.Productos.AddAsync(productos1);
            return Ok();
        }

        private async Task<string> GuardarFoto(IFormFile foto)
        {
            //using var stream = new MemoryStream();
            string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
           // await foto.CopyToAsync(stream);

            //var fileBytes = stream.ToArray();
            string rutaArchivo = Path.Combine("C://Users//monte//OneDrive//Documentos//Back-end//Back-End-PIA//GestionTienda//Imagenes//", nombreArchivo);
            // Aquí debes implementar la lógica para guardar el archivo en tu sistema de almacenamiento (por ejemplo, sistema de archivos, almacenamiento en la nube, etc.)
            // Retorna la URL de la imagen guardada

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
            string urlImagen = "https://localhost:7236/swagger/index.html" + nombreArchivo;
            return urlImagen;
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

