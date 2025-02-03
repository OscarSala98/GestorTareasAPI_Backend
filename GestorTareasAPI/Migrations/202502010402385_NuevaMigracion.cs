namespace GestorTareasAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevaMigracion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "ContraseniaHash", c => c.String(nullable: false));
            AlterColumn("dbo.Rols", "Nombre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tareas", "Titulo", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tareas", "Descripcion", c => c.String(maxLength: 500));
            AlterColumn("dbo.Tareas", "Estado", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Usuarios", "Apellido", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Usuarios", "Correo", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Usuarios", "Contrasenia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "Contrasenia", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Correo", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Apellido", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.Tareas", "Estado", c => c.String(nullable: false));
            AlterColumn("dbo.Tareas", "Descripcion", c => c.String());
            AlterColumn("dbo.Tareas", "Titulo", c => c.String(nullable: false));
            AlterColumn("dbo.Rols", "Nombre", c => c.String(nullable: false));
            DropColumn("dbo.Usuarios", "ContraseniaHash");
        }
    }
}
