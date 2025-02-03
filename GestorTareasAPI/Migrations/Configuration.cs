namespace GestorTareasAPI.Migrations
{
    using GestorTareasAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;
    using static GestorTareasAPI.Models.Tarea;

    internal sealed class Configuration : DbMigrationsConfiguration<GestorTareasAPI.DAL.GestorTareasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GestorTareasAPI.DAL.GestorTareasContext context)
        {
            // Crear roles si no existen
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new[]
                {
            new Rol { Nombre = "Administrador" },
            new Rol { Nombre = "Miembro" }
        });
                context.SaveChanges(); // Guardar roles primero
            }

            // Crear usuarios si no existen
            if (!context.Usuarios.Any())
            {
                var usuarioJuan = new Usuario
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Correo = "juan@example.com",
                    RolId = 1 // Asegúrate de que este RolId exista
                };
                usuarioJuan.SetContrasenia("Admin123"); // Establecer contraseña

                var usuarioMaria = new Usuario
                {
                    Nombre = "Maria",
                    Apellido = "Gómez",
                    Correo = "maria@example.com",
                    RolId = 2 // Asegúrate de que este RolId exista
                };
                usuarioMaria.SetContrasenia("Miembro123"); // Establecer contraseña

                context.Usuarios.AddRange(new[] { usuarioJuan, usuarioMaria });
                context.SaveChanges();
            }

            // Crear tareas si no existen
            if (!context.Tareas.Any())
            {
                context.Tareas.AddRange(new[]
                {
            new Tarea
            {
                Titulo = "Revisar informe",
                Descripcion = "Revisar el informe final del proyecto",
                Estado = EstadoTarea.Pendiente,
                FechaLimite = DateTime.ParseExact("2025-02-15", "yyyy-MM-dd", CultureInfo.InvariantCulture), // Formato corregido
                UsuarioId = 1 // Asegúrate de que este UsuarioId exista
            },
            new Tarea
            {
                Titulo = "Actualizar documentación",
                Descripcion = "Actualizar el manual del sistema",
                Estado = EstadoTarea.En_Progreso,
                FechaLimite = DateTime.ParseExact("2025-02-20", "yyyy-MM-dd", CultureInfo.InvariantCulture), // Formato corregido
                UsuarioId = 2 // Asegúrate de que este UsuarioId exista
            }
        });
                context.SaveChanges();
            }
        }
    }
}
