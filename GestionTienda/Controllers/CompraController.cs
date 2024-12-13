using System;
using AutoMapper;
using GestionTienda.DTOs;
using GestionTienda.Entidades;
using GestionTienda.Mapping;
using GestionTienda.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionTienda.Controllers
{
    [ApiController]
    [Route("/Compra")]
	public class CompraController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;
        private readonly EmailService _emailService;
        public CompraController(AplicationDbContext context, EmailService emailService)
        {
            this.dbContext = context;
            _emailService = emailService;
            // this.configuration = configuration;
        }
       

        [HttpGet]
        public async Task<List<compraDTO>> Get()
        {
            Automapper.Configure();
            var compras = await dbContext.Compra.ToListAsync();

            return Mapper.Map<List<compraDTO>>(compras);
        }

        
        [HttpPost("Compra")]
        public async Task<ActionResult> Post([FromForm] compraDTO comprasDTO)
        {
            Automapper.Configure();
            var comp = Mapper.Map<Compra>(comprasDTO);
            await dbContext.Compra.AddAsync(comp);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
        /*
        [HttpPost("compra")]
        public async Task<ActionResult> Post(compraDTO comprasDTO)
        {
            if (comprasDTO.id_compra == null)
            {
                return BadRequest("No se puede crear un carrito sin productos.");
            }

            var Id_compra = await dbContext.Compra
                .Where(compras123 => comprasDTO.id_compra.Contains(compras123.id_compra)).Select(x => x.Id).ToListAsync();

            if (compraDTO.id_compra.Count != Id_compra.Count)
            {
                return BadRequest("No existe uno de los alumnos enviados");
            }
            var comp = mapper.Map<Comp>(comprasDTO);
            ordenar(comp);
            dbContext.Add(comp);
            await dbContext.SaveChangesAsync();
            var claseDTO = mapper.Map<ClaseDTO>(comp);
            return CreatedAtRoute("obtenerClase", new { id = comp.Id }, claseDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Post(ClaseCreacionDTO claseCreacionDTO)
        {
            if (claseCreacionDTO.AlumnosIds == null)
            {
                return BadRequest("No se puede crear una clase sin alumnos.");
            }
            var alumnosIds = await dbContext.Alumnos
                .Where(alumnoBD => claseCreacionDTO.AlumnosIds.Contains(alumnoBD.Id)).Select(x => x.Id).ToListAsync();
            if (claseCreacionDTO.AlumnosIds.Count != alumnosIds.Count)
            {
                return BadRequest("No existe uno de los alumnos enviados");
            }
            var clase = mapper.Map<Clase>(claseCreacionDTO);
            OrdenarPorAlumnos(clase);
            dbContext.Add(clase);
            await dbContext.SaveChangesAsync();
            var claseDTO = mapper.Map<ClaseDTO>(clase);
            return CreatedAtRoute("obtenerClase", new { id = clase.Id }, claseDTO);
        }

        */
        [HttpGet("Compra/{id}")]//busvca el id del producto
        public IActionResult GetCompraId(int id)
        {
            List<Compra> compras= dbContext.Compra.Where(p => p.id_compra == id).ToList();
            if (compras.Count == 0)
            {
                return NotFound();
            }
            return Ok(compras);
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

      
        [HttpPut("api/Compra/{orderId}")]
        public ActionResult UpdateOrderStatus(int orderId, string newStatus)
        {
            // Lógica para actualizar el estado del pedido en la base de datos

            // Obtener la dirección de correo electrónico del usuario asociado al pedido
            string emailAddress = GetCustomerEmailAddress(orderId);

            // Enviar notificación por correo electrónico al usuario
            _emailService.SendOrderStatusNotification(emailAddress, orderId.ToString(), newStatus);

            return Ok();
        }

        private string GetCustomerEmailAddress(int orderId)
        {
            // Lógica para obtener la dirección de correo electrónico del usuario asociado al pedido
            // Aquí puedes consultar la base de datos o cualquier otro medio para obtener la dirección de correo electrónico

            return "monterrey-luis@hotmail.com";
        }
    }
}

