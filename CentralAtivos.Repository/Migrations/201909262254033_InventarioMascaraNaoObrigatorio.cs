namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventarioMascaraNaoObrigatorio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inventario", "MascaraNomeItem", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Inventario", "MascaraNomeItem", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
