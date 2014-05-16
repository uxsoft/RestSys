namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriticalQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSStocks", "CriticalQuantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSStocks", "CriticalQuantity");
        }
    }
}
