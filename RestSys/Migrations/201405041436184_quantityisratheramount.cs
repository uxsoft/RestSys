namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quantityisratheramount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSStockItems", "Amount", c => c.Double(nullable: false));
            DropColumn("dbo.RSStockItems", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RSStockItems", "Quantity", c => c.Double(nullable: false));
            DropColumn("dbo.RSStockItems", "Amount");
        }
    }
}
