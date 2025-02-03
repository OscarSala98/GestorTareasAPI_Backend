using GestorTareasAPI.DAL;
using GestorTareasAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using static GestorTareasAPI.Models.Tarea;

namespace GestorTareasAPI.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class TareasController : ApiController
    {
        private GestorTareasContext db = new GestorTareasContext();

        [HttpGet]
        public IHttpActionResult ObtenerTareas()
        {
            var tareas = db.Tareas.Include(t => t.Usuario)
         .Select(t => new
         {
             t.Id,
             t.Titulo,
             t.Descripcion,
             Estado = t.Estado.ToString(),  // 👈 Convierte el Enum a string
             t.FechaLimite,
             Usuario = new
             {
                 t.Usuario.Id,
                 t.Usuario.Nombre,
                 t.Usuario.Apellido
             }
         }).ToList();

            return Ok(tareas);
        }

        [HttpGet]
        [Route("api/tareas/{id}")]
        public IHttpActionResult ObtenerTarea(int id)
        {
            var tarea = db.Tareas.Include(t => t.Usuario)
        .Where(t => t.Id == id)
        .Select(t => new
        {
            t.Id,
            t.Titulo,
            t.Descripcion,
            Estado = t.Estado.ToString(),  // 👈 Convierte el Enum a string
            t.FechaLimite,
            Usuario = new
            {
                t.Usuario.Id,
                t.Usuario.Nombre,
                t.Usuario.Apellido
            }
        }).FirstOrDefault();

            if (tarea == null) return NotFound();

            return Ok(tarea);
        }

        [HttpGet]
        [Route("api/tareas/filter")]
        public IHttpActionResult ObtenerTareasPorEstado(string estado)
        {
            if (!Enum.TryParse(estado, out EstadoTarea estadoEnum))
                return BadRequest("Estado no válido.");

            var tareasFiltradas = db.Tareas
                .Where(t => t.Estado == estadoEnum)
                .Select(t => new
                {
                    t.Id,
                    t.Titulo,
                    t.Descripcion,
                    Estado = t.Estado.ToString(), // 👈 Convierte el Enum a string
                    t.FechaLimite,
                    Usuario = new
                    {
                        t.Usuario.Id,
                        t.Usuario.Nombre,
                        t.Usuario.Apellido
                    }
                }).ToList();

            return tareasFiltradas.Count == 0 ? (IHttpActionResult)NotFound() : Ok(tareasFiltradas);
        }

        [HttpPost]
        public IHttpActionResult CrearTarea(Tarea tarea)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Tareas.Add(tarea);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = tarea.Id }, tarea);
        }

        [HttpPut]
        [Route("api/tareas/{id}")]
        public IHttpActionResult ActualizarTarea(int id, Tarea tarea)
        {
            if (!ModelState.IsValid || id != tarea.Id) return BadRequest();

            var tareaExistente = db.Tareas.Find(id);
            if (tareaExistente == null) return NotFound();

            tareaExistente.Titulo = tarea.Titulo;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.FechaLimite = tarea.FechaLimite;
            tareaExistente.Estado = tarea.Estado;

            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("api/tareas/{id}")]
        public IHttpActionResult BorrarTarea(int id)
        {
            var tarea = db.Tareas.Find(id);
            if (tarea == null) return NotFound();

            db.Tareas.Remove(tarea);
            db.SaveChanges();
            return Ok(tarea);
        }
    }
}
