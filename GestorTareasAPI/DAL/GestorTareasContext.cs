using System.Data.Entity;
using GestorTareasAPI.Models;

namespace GestorTareasAPI.DAL
{
    public class GestorTareasContext : DbContext
    {
        public GestorTareasContext() : base("GestorTareasDB") { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}