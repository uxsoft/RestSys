namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockitemrelationsrequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RSStockItems", "Product_Id", "dbo.RSProducts");
            DropForeignKey("dbo.RSStockItems", "Stock_Id", "dbo.RSStocks");
            DropIndex("dbo.RSStockItems", new[] { "Product_Id" });
            DropIndex("dbo.RSStockItems", new[] { "Stock_Id" });
            AlterColumn("dbo.RSStockItems", "Product_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.RSStockItems", "Stock_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.RSStockItems", "Product_Id");
            CreateIndex("dbo.RSStockItems", "Stock_Id");
            AddForeignKey("dbo.RSStockItems", "Product_Id", "dbo.RSProducts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RSStockItems", "Stock_Id", "dbo.RSStocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RSStockItems", "Stock_Id", "dbo.RSStocks");
            DropForeignKey("dbo.RSStockItems", "Product_Id", "dbo.RSProducts");
            DropIndex("dbo.RSStockItems", new[] { "Stock_Id" });
            DropIndex("dbo.RSStockItems", new[] { "Product_Id" });
            AlterColumn("dbo.RSStockItems", "Stock_Id", c => c.Int());
            AlterColumn("dbo.RSStockItems", "Product_Id", c => c.Int());
            CreateIndex("dbo.RSStockItems", "Stock_Id");
            CreateIndex("dbo.RSStockItems", "Product_Id");
            AddForeignKey("dbo.RSStockItems", "Stock_Id", "dbo.RSStocks", "Id");
            AddForeignKey("dbo.RSStockItems", "Product_Id", "dbo.RSProducts", "Id");
        }
    }
}
