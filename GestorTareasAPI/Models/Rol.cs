using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GestorTareasAPI.Models
{
    [Table("Roles")]
    public class Rol
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        // Relación con Usuarios
        [JsonIgnore] // Evita el bucle de referencia
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}