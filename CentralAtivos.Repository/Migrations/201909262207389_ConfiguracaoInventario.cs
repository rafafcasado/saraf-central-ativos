namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguracaoInventario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventarioConfig",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        EntidadeCampoID = c.Int(nullable: false),
                        Visivel = c.Boolean(nullable: false),
                        Obrigatorio = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EntidadeCampo", t => t.EntidadeCampoID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .Index(t => t.InventarioID)
                .Index(t => t.EntidadeCampoID);
            
            AddColumn("dbo.Inventario", "MascaraNomeItem", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Inventario", "AplicarMascara", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventarioConfig", "InventarioID", "dbo.Inventario");
            DropForeignKey("dbo.InventarioConfig", "EntidadeCampoID", "dbo.EntidadeCampo");
            DropIndex("dbo.InventarioConfig", new[] { "EntidadeCampoID" });
            DropIndex("dbo.InventarioConfig", new[] { "InventarioID" });
            DropColumn("dbo.Inventario", "AplicarMascara");
            DropColumn("dbo.Inventario", "MascaraNomeItem");
            DropTable("dbo.InventarioConfig");
        }
    }
}
