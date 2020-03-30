namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "StatusID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "StatusID");
        }
    }
}
