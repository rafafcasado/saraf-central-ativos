namespace CentralAtivos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpresaPermiteCodigosIguais : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "PermiteCodigosIguais", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "PermiteCodigosIguais");
        }
    }
}
