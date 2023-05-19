using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionTienda.DTOs;
using GestionTienda.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GestionTienda.Controllers
{
    [ApiController]
    [Route("Usuario")]
	public class UsuarioController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AplicationDbContext dbContext;

		public UsuarioController(AplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {

            this.dbContext = context;
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await dbContext.Usuario.Include(x=> x.carritos).ToListAsync();
        }
		
		[HttpPost]
		public async Task<ActionResult> Post(Usuario usuario)
		{
		    dbContext.Add(usuario);
		    await dbContext.SaveChangesAsync();
		    return Ok();
		}

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(Usuario usuario, int id)
        {
            if (usuario.id_usuario != id)
            {
                return BadRequest("El id del usuario no coincide con el establecido en la url");
            }

            dbContext.Update(usuario);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Usuario.AnyAsync(x => x.id_usuario == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Usuario()
            {
                id_usuario = id
            });
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Registro")]
        public async Task<ActionResult<RespAunt>> Registrar(CredenUs credenUs)
        {
            var user = new IdentityUser { UserName = credenUs.Email, Email = credenUs.Email };
            var result = await userManager.CreateAsync(user, credenUs.Password);

            if (result.Succeeded)
            {
                //Se retorna el Jwt (Json Web Token) especifica el formato del token que hay que devolverle a los clientes
                return await ConstruirToken(credenUs);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }



        private async Task<RespAunt> ConstruirToken(CredenUs credenUs)
        {
            //Informacion del usuario en la cual podemos confiar
            //En los claim se pueden declarar cualquier variable, sin embargo, no debemos de declarar informacion
            //del cliente sensible como pudiera ser una Tarjeta de Credito o contraseña

            var claims = new List<Claim>
            {
                new Claim("email", credenUs.Email),
                new Claim("claimprueba", "Este es un claim de prueba")
            };

            var usuario = await userManager.FindByEmailAsync(credenUs.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new RespAunt()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }
    }
}

