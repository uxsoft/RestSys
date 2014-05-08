namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockUnitMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSStockItems", "Unit", c => c.String());
            AddColumn("dbo.RSStocks", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSStocks", "Unit");
            DropColumn("dbo.RSStockItems", "Unit");
        }
    }
}
