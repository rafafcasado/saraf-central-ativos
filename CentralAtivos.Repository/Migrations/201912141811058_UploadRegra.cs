namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadRegra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Upload", "RegraUploadID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Upload", "RegraUploadID");
        }
    }
}
