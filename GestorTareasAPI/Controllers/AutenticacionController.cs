using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using GestorTareasAPI.Models;
using GestorTareasAPI.DAL;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;

namespace GestorTareasAPI.Controllers
{
    [RoutePrefix("api/autenticacion")]
    public class AutenticacionController : ApiController
    {
        private readonly GestorTareasContext db = new GestorTareasContext();

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginModel login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = db.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.Correo == login.Correo);
            if (usuario == null || !Usuario.VerificarContrasenia(login.Contrasenia, usuario.ContraseniaHash))
                return Unauthorized();

            var token = GenerarToken(usuario);
            return Ok(new { Token = token });
        }

        private string GenerarToken(Usuario usuario)
        {
            var secret = ConfigurationManager.AppSettings["JwtSecret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.RolId.ToString())
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(claims: claims, signingCredentials: creds, expires: 
                DateTime.UtcNow.AddHours(2)));
        }
    }
}
