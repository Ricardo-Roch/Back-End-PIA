using System;
using AutoMapper;
using GestionTienda.DTOs;
using GestionTienda.Entidades;
using GestionTienda.Mapping;
using GestionTienda.Migrations;
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
            string fotoUrl = string.Empty;
            var producto = await dbContext.Productos.ToListAsync();
            return Mapper.Map<List<productoDTO>>(producto); ;
          
        }/*
        [HttpGet("Imagen/{nombreImagen}")]
        public IActionResult ObtenerImagen(string nombreImagen, IFormFile foto)
        {
             nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            var rutaImagen = Path.Combine("C://Users//monte//OneDrive//Documentos//Back-end//Back-End-PIA//GestionTienda//Imagenes//", nombreImagen + ".jpg");

            if (!System.IO.File.Exists(rutaImagen))
            {
                return NotFound();
            }

            var imagenBytes = System.IO.File.ReadAllBytes(rutaImagen);
            return File(imagenBytes, "image/jpeg");
        }*/
        [HttpGet("Imagen/{idProducto}")]
        public IActionResult ObtenerImagen(int idProducto)
        {
            // Obtener el producto por su ID
            var producto = dbContext.Productos.FirstOrDefault(p => p.id_producto == idProducto);

            if (producto == null)
            {
                return NotFound();
            }

            var nombreImagen = producto.Imagen;
            var rutaImagen = Path.Combine("C://Users//monte//OneDrive//Documentos//Back-end//Back-End-PIA//GestionTienda//Imagenes//", nombreImagen);

            if (!System.IO.File.Exists(rutaImagen))
            {
                return NotFound();
            }

            var imagenBytes = System.IO.File.ReadAllBytes(rutaImagen);
            return File(imagenBytes, "image/jpeg");
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
        public async Task<IActionResult> CrearProducto([FromForm] GetProducto productoDTO)
        {
            string fotoUrl = string.Empty;

            if (productoDTO.Imagen != null)
            {
                fotoUrl = await GuardarFoto(productoDTO.Imagen);
            }

            var producto = new Productos
            {

                id_producto = productoDTO.id_producto,
                disponibilidad = productoDTO.disponibilidad,
                categoria = productoDTO.categoria,
                Nombre_producto = productoDTO.Nombre_producto,
                Imagen = fotoUrl,
                Id_carrito = productoDTO.Id_carrito // Asignar el Id_carrito al producto

        };

            // Guardar el producto en la base de datos
            dbContext.Productos.Add(producto);
            await dbContext.SaveChangesAsync();

            return Ok();
        }



        private async Task<string> GuardarFoto(IFormFile foto)
        {
            //using var stream = new MemoryStream();
            //string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            string nombreArchivo = foto.FileName;
           // await foto.CopyToAsync(stream);

            //var fileBytes = stream.ToArray();
            string rutaArchivo = Path.Combine("C://Users//monte//OneDrive//Documentos//Back-end//Back-End-PIA//GestionTienda//Imagenes//", nombreArchivo);
            // Aquí debes implementar la lógica para guardar el archivo en tu sistema de almacenamiento (por ejemplo, sistema de archivos, almacenamiento en la nube, etc.)
            // Retorna la URL de la imagen guardada

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
          //  string urlImagen = "https://localhost:7236/swagger/index.html" + nombreArchivo;
            string urlImagen = nombreArchivo;
            return urlImagen;
        }


        /*[HttpPut("{id:int}")]
        public async Task<ActionResult> put(Productos productos, int id)
        {
            if (productos.id_producto != id)
            {
                return BadRequest("El id del producto no coincide con el establecido en la url");
            }

            dbContext.Update(productos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }*/
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromForm] GetProducto productoDTO)
        {
            var productoExistente = await dbContext.Productos.FindAsync(id);

            if (productoExistente == null)
            {
                return NotFound();
            }

            // Procesar y guardar la imagen si se proporciona una nueva imagen
            if (productoDTO.Imagen != null)
            {
                string fotoUrl = await GuardarFoto(productoDTO.Imagen);

                // Actualizar la propiedad Imagen con la nueva URL de la imagen
                productoExistente.Imagen = fotoUrl;
            }

            // Actualizar otras propiedades del producto si es necesario
            productoExistente.disponibilidad = productoDTO.disponibilidad;
            productoExistente.Id_carrito = productoDTO.Id_carrito;
            productoExistente.categoria = productoDTO.categoria;
            productoExistente.Nombre_producto = productoDTO.Nombre_producto;

            // Guardar los cambios en la base de datos
            dbContext.Productos.Update(productoExistente);
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
        /*
        [HttpGet("recomendaciones")]
        public async Task<ActionResult<Productos>> GetRecomendacionProducto(int idUsuario, string categoria)
        {
            // Obtener el carrito del usuario por su ID
            var carrito = await dbContext.Carrito
                .Include(c => c.productos)
                .FirstOrDefaultAsync(c => c.id_usuario == idUsuario);

            if (carrito == null)
            {
                return NotFound("No se encontró el carrito del usuario");
            }

            // Obtener los ID de los productos en el carrito
            var productosEnCarrito = carrito.productos
                .Select(p => p.id_producto)
                .ToList();

            // Obtener un producto de la misma categoría que no esté en el carrito
            var productoRecomendado = await dbContext.Productos
                .Where(p => p.categoria == categoria && !productosEnCarrito.Contains(p.id_producto))
                .FirstOrDefaultAsync();

            if (productoRecomendado == null)
            {
                // No se encontraron productos recomendados
                return NotFound("No se encontraron productos recomendados");
            }

            return Ok(productoRecomendado);
        }
        */
        /*
        [HttpGet("recomendaciones")]
        public async Task<ActionResult<Productos>> GetRecomendacionProducto(int idUsuario)
        {
            // Obtener el carrito del usuario por su ID
            var carrito = await dbContext.Carrito
                .Include(c => c.productos)
                .FirstOrDefaultAsync(c => c.id_usuario == idUsuario);

            if (carrito == null)
            {
                return NotFound("No se encontró el carrito del usuario");
            }

            // Obtener los ID de los productos en el carrito
            var productosEnCarrito = carrito.productos
                .Select(p => p.id_producto)
                .ToList();

            // Obtener todos los productos que no están en el carrito
            var productosDisponibles = await dbContext.Productos
                .Where(p => !productosEnCarrito.Contains(p.id_producto))
                .ToListAsync();

            // Verificar si existen productos disponibles
            if (productosDisponibles.Count == 0)
            {
                // No se encontraron productos recomendados
                return NotFound("No se encontraron productos recomendados");
            }

            // Seleccionar un producto al azar de los disponibles
            var random = new Random();
            var productoRecomendado = productosDisponibles[random.Next(productosDisponibles.Count)];

            return Ok(productoRecomendado);
        }
        */
        [HttpGet("recomendaciones")]
        public async Task<ActionResult<Productos>> GetRecomendacionProducto(int idUsuario)
        {
            // Obtener el carrito del usuario por su ID
            var carrito = await dbContext.Carrito
                .Include(c => c.productos)
                .FirstOrDefaultAsync(c => c.id_usuario == idUsuario);

            if (carrito == null)
            {
                return NotFound("No se encontró el carrito del usuario");
            }

            // Obtener los ID de los productos en el carrito
            var productosEnCarrito = carrito.productos
                .Select(p => p.id_producto)
                .ToList();

            // Obtener las categorías de los productos en el carrito
            var categoriasEnCarrito = carrito.productos
                .Select(p => p.categoria)
                .ToList();

            // Obtener la categoría que predomine en el carrito
            var categoriaPredominante = categoriasEnCarrito
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            if (categoriaPredominante == null)
            {
                return NotFound("No se encontró ninguna categoría predominante en el carrito");
            }

            // Obtener un producto de la categoría predominante que no está en el carrito
            var productoRecomendado = await dbContext.Productos
                .FirstOrDefaultAsync(p => p.categoria == categoriaPredominante && !productosEnCarrito.Contains(p.id_producto));

            if (productoRecomendado == null)
            {
                return NotFound("No se encontró ningún producto recomendado");
            }

            return Ok(productoRecomendado);
        }


    }
}

