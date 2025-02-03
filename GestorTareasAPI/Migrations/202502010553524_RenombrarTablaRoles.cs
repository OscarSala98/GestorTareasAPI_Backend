namespace GestorTareasAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenombrarTablaRoles : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rols", newName: "Roles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Roles", newName: "Rols");
        }
    }
}
