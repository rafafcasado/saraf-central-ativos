namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescricaoTela : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tela", "Descricao", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tela", "Descricao");
        }
    }
}
