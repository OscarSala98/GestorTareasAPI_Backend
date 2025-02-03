using GestorTareasAPI.DAL;
using GestorTareasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;

namespace GestorTareasAPI.Controllers
{
    [Authorize] // Protege todas las rutas con JWT
    [AllowAnonymous]
    public class UsuariosController : ApiController
    {
        private GestorTareasContext db = new GestorTareasContext();

        [HttpGet]
        //[Authorize(Roles = "1")]
        
        //[Authorize(Roles = "Administrador")]
        public IHttpActionResult ObtenerUsuarios()
        {
            var usuarios = db.Usuarios.Include(u => u.Rol).ToList();
            return Ok(usuarios);
        }

        [HttpGet]
        public IHttpActionResult ObtenerUsuario(int id)
        {
            var usuario = db.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public IHttpActionResult CrearUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            usuario.ContraseniaHash = Usuario.HashContrasenia(usuario.ContraseniaHash); // Hashear contraseña correctamente

            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid || id != usuario.Id) return BadRequest();

            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult BorrarUsuario(int id)
        {
            var usuario = db.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return Ok(usuario);
        }
    }
}
