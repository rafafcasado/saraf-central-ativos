namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TamanhoNumeroPlaca : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlacaGrupo", "Tamanho", c => c.Int(nullable: false));
            AddColumn("dbo.PlacaGrupo", "AplicaZerosEsquerda", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlacaGrupo", "AplicaZerosEsquerda");
            DropColumn("dbo.PlacaGrupo", "Tamanho");
        }
    }
}
