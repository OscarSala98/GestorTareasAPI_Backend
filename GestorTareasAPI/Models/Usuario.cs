using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using BCrypt;

namespace GestorTareasAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        public string Correo { get; set; }

        [Required]
        public string ContraseniaHash { get; set; } // Almacenamos la contraseña encriptada


        [Required]
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }


        // Método para establecer la contraseña de forma segura
        public static string HashContrasenia(string contrasenia)
        {
            if (contrasenia.Length < 6 || contrasenia.Length > 20) // 🔹 Solo valida la contraseña original, NO el hash
                throw new ValidationException("La contraseña debe tener entre 6 y 20 caracteres.");

            return BCrypt.Net.BCrypt.HashPassword(contrasenia); // 🔹 Encripta la contraseña después de validarla
        }


        // Método estático para verificar contraseñas
        public static bool VerificarContrasenia(string contrasenia, string hashAlmacenado)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenia, hashAlmacenado);
        }
        public void SetContrasenia(string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(contrasenia))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            ContraseniaHash = BCrypt.Net.BCrypt.HashPassword(contrasenia);
        }
    }
}