using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestorTareasAPI.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Titulo { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required]
        public EstadoTarea Estado { get; set; }

        [Required]
        [DataType(DataType.Date)]
        
        public DateTime FechaLimite { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public enum EstadoTarea
        {
            Pendiente,
            En_Progreso,
            Completada
        }
    }
}