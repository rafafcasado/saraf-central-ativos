namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventarioItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventarioItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        DataColeta = c.DateTime(nullable: false),
                        UsuarioColetaID = c.Int(nullable: false),
                        DataUltimaAtualizacao = c.DateTime(nullable: false),
                        UsuarioUltimaAtualizacaoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .ForeignKey("dbo.Item", t => t.ItemID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioColetaID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioUltimaAtualizacaoID)
                .Index(t => t.InventarioID)
                .Index(t => t.ItemID)
                .Index(t => t.UsuarioColetaID)
                .Index(t => t.UsuarioUltimaAtualizacaoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventarioItem", "UsuarioUltimaAtualizacaoID", "dbo.Usuario");
            DropForeignKey("dbo.InventarioItem", "UsuarioColetaID", "dbo.Usuario");
            DropForeignKey("dbo.InventarioItem", "ItemID", "dbo.Item");
            DropForeignKey("dbo.InventarioItem", "InventarioID", "dbo.Inventario");
            DropIndex("dbo.InventarioItem", new[] { "UsuarioUltimaAtualizacaoID" });
            DropIndex("dbo.InventarioItem", new[] { "UsuarioColetaID" });
            DropIndex("dbo.InventarioItem", new[] { "ItemID" });
            DropIndex("dbo.InventarioItem", new[] { "InventarioID" });
            DropTable("dbo.InventarioItem");
        }
    }
}
