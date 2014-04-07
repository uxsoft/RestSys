namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RSNavigationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Parent_Id = c.Int(),
                        ProductLink_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSNavigationItems", t => t.Parent_Id)
                .ForeignKey("dbo.RSProducts", t => t.ProductLink_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.ProductLink_Id);
            
            CreateTable(
                "dbo.RSProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        ShowOnMenu = c.Boolean(nullable: false),
                        SerialNumber = c.String(),
                        Amount = c.Double(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSOrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Order_Id = c.Int(),
                        Receipt_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSOrders", t => t.Order_Id)
                .ForeignKey("dbo.RSReceipts", t => t.Receipt_Id)
                .ForeignKey("dbo.RSProducts", t => t.Product_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Receipt_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.RSOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderedOn = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSReceipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        Order_Id = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSOrders", t => t.Order_Id)
                .ForeignKey("dbo.RSUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RSUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsWaiter = c.Boolean(nullable: false),
                        IsDiscountManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Notes = c.String(),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSStyles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Path = c.String(),
                        Type = c.Int(nullable: false),
                        Title = c.String(),
                        Notes = c.String(),
                        Selected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSStockRSProducts",
                c => new
                    {
                        RSStock_Id = c.Int(nullable: false),
                        RSProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RSStock_Id, t.RSProduct_Id })
                .ForeignKey("dbo.RSStocks", t => t.RSStock_Id, cascadeDelete: true)
                .ForeignKey("dbo.RSProducts", t => t.RSProduct_Id, cascadeDelete: true)
                .Index(t => t.RSStock_Id)
                .Index(t => t.RSProduct_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RSStockRSProducts", "RSProduct_Id", "dbo.RSProducts");
            DropForeignKey("dbo.RSStockRSProducts", "RSStock_Id", "dbo.RSStocks");
            DropForeignKey("dbo.RSOrderItems", "Product_Id", "dbo.RSProducts");
            DropForeignKey("dbo.RSReceipts", "User_Id", "dbo.RSUsers");
            DropForeignKey("dbo.RSOrderItems", "Receipt_Id", "dbo.RSReceipts");
            DropForeignKey("dbo.RSReceipts", "Order_Id", "dbo.RSOrders");
            DropForeignKey("dbo.RSOrderItems", "Order_Id", "dbo.RSOrders");
            DropForeignKey("dbo.RSNavigationItems", "ProductLink_Id", "dbo.RSProducts");
            DropForeignKey("dbo.RSNavigationItems", "Parent_Id", "dbo.RSNavigationItems");
            DropIndex("dbo.RSStockRSProducts", new[] { "RSProduct_Id" });
            DropIndex("dbo.RSStockRSProducts", new[] { "RSStock_Id" });
            DropIndex("dbo.RSOrderItems", new[] { "Product_Id" });
            DropIndex("dbo.RSReceipts", new[] { "User_Id" });
            DropIndex("dbo.RSOrderItems", new[] { "Receipt_Id" });
            DropIndex("dbo.RSReceipts", new[] { "Order_Id" });
            DropIndex("dbo.RSOrderItems", new[] { "Order_Id" });
            DropIndex("dbo.RSNavigationItems", new[] { "ProductLink_Id" });
            DropIndex("dbo.RSNavigationItems", new[] { "Parent_Id" });
            DropTable("dbo.RSStockRSProducts");
            DropTable("dbo.RSStyles");
            DropTable("dbo.RSStocks");
            DropTable("dbo.RSUsers");
            DropTable("dbo.RSReceipts");
            DropTable("dbo.RSOrders");
            DropTable("dbo.RSOrderItems");
            DropTable("dbo.RSProducts");
            DropTable("dbo.RSNavigationItems");
        }
    }
}
