namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Placas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Placa",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlacaGrupoID = c.Int(nullable: false),
                        NumeroPlaca = c.Int(nullable: false),
                        ItemID = c.Int(),
                        Observacao = c.String(maxLength: 250),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PlacaGrupo", t => t.PlacaGrupoID)
                .Index(t => t.PlacaGrupoID);
            
            CreateTable(
                "dbo.PlacaGrupo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InventarioID = c.Int(nullable: false),
                        Inicio = c.Int(nullable: false),
                        Fim = c.Int(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventario", t => t.InventarioID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.InventarioID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Placa", "PlacaGrupoID", "dbo.PlacaGrupo");
            DropForeignKey("dbo.PlacaGrupo", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PlacaGrupo", "InventarioID", "dbo.Inventario");
            DropIndex("dbo.PlacaGrupo", new[] { "UsuarioID" });
            DropIndex("dbo.PlacaGrupo", new[] { "InventarioID" });
            DropIndex("dbo.Placa", new[] { "PlacaGrupoID" });
            DropTable("dbo.PlacaGrupo");
            DropTable("dbo.Placa");
        }
    }
}
