namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coletor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coletor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(maxLength: 50),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Marca = c.String(maxLength: 50),
                        Modelo = c.String(maxLength: 50),
                        MacAddress = c.String(nullable: false, maxLength: 20),
                        DataCadastro = c.DateTime(nullable: false),
                        DataExclusao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Coletor");
        }
    }
}
