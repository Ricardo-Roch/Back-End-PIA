using System;
using GestionTienda.DTOs;
using GestionTienda.Services;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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

