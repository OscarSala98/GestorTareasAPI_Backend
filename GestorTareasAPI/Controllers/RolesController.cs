using GestorTareasAPI.DAL;
using GestorTareasAPI.Models;
using System.Linq;
using System.Web.Http;

namespace GestorTareasAPI.Controllers
{
    [Authorize]
    public class RolesController : ApiController
    {
        private GestorTareasContext db = new GestorTareasContext();

        [HttpGet]
        public IHttpActionResult ObtenerRoles()
        {
            return Ok(db.Roles.ToList());
        }

        [HttpGet]
        [Route("api/roles/{id}")]
        public IHttpActionResult ObtenerRol(int id)
        {
            var rol = db.Roles.Find(id);
            if (rol == null) return NotFound();

            return Ok(rol);
        }
    }
}
