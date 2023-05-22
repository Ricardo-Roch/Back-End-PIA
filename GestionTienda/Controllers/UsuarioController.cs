using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using GestionTienda.DTOs;
using GestionTienda.Entidades;
using GestionTienda.Mapping;
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
        //metodo get, mapeo manual jalando al 100
        /*
        [HttpGet("Con dtos MANUALES")]
        public async Task<List<UsuarioDTO>> Get()
        {
            var usuarios = await dbContext.Usuario.ToListAsync();
            var usuariodto = new List<UsuarioDTO>();
            foreach(var usuario in usuarios)
            {
                usuariodto.Add(new UsuarioDTO { id_usuario = usuario.id_usuario, nombre = usuario.nombre, carritos = usuario.carritos, contra = usuario.contra, correo = usuario.correo });
            }

            return usuariodto;
        }   */
        
		  [HttpGet("Sin DTOs'MANUALES'")]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await dbContext.Usuario.Include(x=> x.carritos).ToListAsync();
        }


        /*
        [HttpPost]
		public async Task<ActionResult> Post(Usuario usuario)
		{
		    dbContext.Add(usuario);
		    await dbContext.SaveChangesAsync();
		    return Ok();
		}*/

        [HttpPost]
        public async Task<ActionResult> Post(GetUsuarioDTO getUsuarioDTO)
        {
            Automapper.Configure();
            var us = Mapper.Map<Usuario>(getUsuarioDTO);
            await dbContext.Usuario.AddAsync(us);
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
        public async Task<ActionResult<RespAunt>> Registro(CredenUs credenUs)
        {
            var user = new IdentityUser { UserName = credenUs.Email , Email = credenUs.Email };
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

        [HttpPost("login")]
        public async Task<ActionResult<RespAunt>> Login(CredenUs credencialesUsuario)
        {
            var result = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }
        [HttpPost("HacerAdmin")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return NoContent();
        }


        private async Task<RespAunt> ConstruirToken(CredenUs credenUs)
        {
            //Informacion del usuario en la cual podemos confiar
            //En los claim se pueden declarar cualquier variable, sin embargo, no debemos de declarar informacion
            //del cliente sensible como pudiera ser una Tarjeta de Credito o contraseña

            var claims = new List<Claim>
            {
                new Claim("Email", credenUs.Email),
                new Claim("Nombre", "Este es un claim de prueba"),
                new Claim("Carrito", "")
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

