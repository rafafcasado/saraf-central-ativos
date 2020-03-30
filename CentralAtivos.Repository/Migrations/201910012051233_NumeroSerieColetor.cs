namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumeroSerieColetor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coletor", "NumeroSerie", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Coletor", "MacAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coletor", "MacAddress", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Coletor", "NumeroSerie");
        }
    }
}
