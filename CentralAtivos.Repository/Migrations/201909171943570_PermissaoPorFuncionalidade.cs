namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissaoPorFuncionalidade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Permissao", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.Permissao", "RequisicaoID", "dbo.Requisicao");
            DropIndex("dbo.Permissao", new[] { "PerfilID" });
            DropIndex("dbo.Permissao", new[] { "RequisicaoID" });
            CreateTable(
                "dbo.Acao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 20),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PerfilTelaAcao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        TelaAcaoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.TelaAcao", t => t.TelaAcaoID)
                .Index(t => t.PerfilID)
                .Index(t => t.TelaAcaoID);
            
            CreateTable(
                "dbo.TelaAcao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TelaID = c.Int(nullable: false),
                        AcaoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Acao", t => t.AcaoID)
                .ForeignKey("dbo.Tela", t => t.TelaID)
                .Index(t => t.TelaID)
                .Index(t => t.AcaoID);
            
            CreateTable(
                "dbo.Tela",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Controller = c.String(nullable: false, maxLength: 50),
                        View = c.String(nullable: false, maxLength: 50),
                        Menu = c.Boolean(nullable: false),
                        NomeMenu = c.String(nullable: false, maxLength: 30),
                        Icone = c.String(nullable: false, maxLength: 20),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PerfilTela",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        TelaID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.Tela", t => t.TelaID)
                .Index(t => t.PerfilID)
                .Index(t => t.TelaID);
            
            CreateTable(
                "dbo.TelaAcaoRequisicao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TelaAcaoID = c.Int(nullable: false),
                        RequisicaoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Requisicao", t => t.RequisicaoID)
                .ForeignKey("dbo.TelaAcao", t => t.TelaAcaoID)
                .Index(t => t.TelaAcaoID)
                .Index(t => t.RequisicaoID);
            
            DropTable("dbo.Permissao");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PerfilID = c.Int(nullable: false),
                        RequisicaoID = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.TelaAcaoRequisicao", "TelaAcaoID", "dbo.TelaAcao");
            DropForeignKey("dbo.TelaAcaoRequisicao", "RequisicaoID", "dbo.Requisicao");
            DropForeignKey("dbo.PerfilTela", "TelaID", "dbo.Tela");
            DropForeignKey("dbo.PerfilTela", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PerfilTelaAcao", "TelaAcaoID", "dbo.TelaAcao");
            DropForeignKey("dbo.TelaAcao", "TelaID", "dbo.Tela");
            DropForeignKey("dbo.TelaAcao", "AcaoID", "dbo.Acao");
            DropForeignKey("dbo.PerfilTelaAcao", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.TelaAcaoRequisicao", new[] { "RequisicaoID" });
            DropIndex("dbo.TelaAcaoRequisicao", new[] { "TelaAcaoID" });
            DropIndex("dbo.PerfilTela", new[] { "TelaID" });
            DropIndex("dbo.PerfilTela", new[] { "PerfilID" });
            DropIndex("dbo.TelaAcao", new[] { "AcaoID" });
            DropIndex("dbo.TelaAcao", new[] { "TelaID" });
            DropIndex("dbo.PerfilTelaAcao", new[] { "TelaAcaoID" });
            DropIndex("dbo.PerfilTelaAcao", new[] { "PerfilID" });
            DropTable("dbo.TelaAcaoRequisicao");
            DropTable("dbo.PerfilTela");
            DropTable("dbo.Tela");
            DropTable("dbo.TelaAcao");
            DropTable("dbo.PerfilTelaAcao");
            DropTable("dbo.Acao");
            CreateIndex("dbo.Permissao", "RequisicaoID");
            CreateIndex("dbo.Permissao", "PerfilID");
            AddForeignKey("dbo.Permissao", "RequisicaoID", "dbo.Requisicao", "ID");
            AddForeignKey("dbo.Permissao", "PerfilID", "dbo.Perfil", "ID");
        }
    }
}
