namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixstockitem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RSStockItemRSProducts", "RSStockItem_Id", "dbo.RSStockItems");
            DropForeignKey("dbo.RSStockItemRSProducts", "RSProduct_Id", "dbo.RSProducts");
            DropIndex("dbo.RSStockItemRSProducts", new[] { "RSStockItem_Id" });
            DropIndex("dbo.RSStockItemRSProducts", new[] { "RSProduct_Id" });
            AddColumn("dbo.RSStockItems", "Product_Id", c => c.Int());
            CreateIndex("dbo.RSStockItems", "Product_Id");
            AddForeignKey("dbo.RSStockItems", "Product_Id", "dbo.RSProducts", "Id");
            DropTable("dbo.RSStockItemRSProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RSStockItemRSProducts",
                c => new
                    {
                        RSStockItem_Id = c.Int(nullable: false),
                        RSProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RSStockItem_Id, t.RSProduct_Id });
            
            DropForeignKey("dbo.RSStockItems", "Product_Id", "dbo.RSProducts");
            DropIndex("dbo.RSStockItems", new[] { "Product_Id" });
            DropColumn("dbo.RSStockItems", "Product_Id");
            CreateIndex("dbo.RSStockItemRSProducts", "RSProduct_Id");
            CreateIndex("dbo.RSStockItemRSProducts", "RSStockItem_Id");
            AddForeignKey("dbo.RSStockItemRSProducts", "RSProduct_Id", "dbo.RSProducts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RSStockItemRSProducts", "RSStockItem_Id", "dbo.RSStockItems", "Id", cascadeDelete: true);
        }
    }
}
