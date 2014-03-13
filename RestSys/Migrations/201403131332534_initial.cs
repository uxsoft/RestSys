namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RSDiscounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                        ModuleId = c.String(nullable: false),
                        Data = c.String(),
                        Notes = c.String(),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.RSUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsWaiter = c.Boolean(nullable: false),
                        IsStorekeeper = c.Boolean(nullable: false),
                        IsDiscountManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOrdered = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        Notes = c.String(),
                        PaymentStatus = c.Int(nullable: false),
                        Waiter_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSUsers", t => t.Waiter_Id, cascadeDelete: true)
                .Index(t => t.Waiter_Id);
            
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
                "dbo.RSShifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Notes = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RSStyles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Title = c.String(),
                        Notes = c.String(),
                        Selected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RSOrderRSDiscounts",
                c => new
                    {
                        RSOrder_Id = c.Int(nullable: false),
                        RSDiscount_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RSOrder_Id, t.RSDiscount_Id })
                .ForeignKey("dbo.RSOrders", t => t.RSOrder_Id, cascadeDelete: true)
                .ForeignKey("dbo.RSDiscounts", t => t.RSDiscount_Id, cascadeDelete: true)
                .Index(t => t.RSOrder_Id)
                .Index(t => t.RSDiscount_Id);
            
            CreateTable(
                "dbo.RSProductRSOrders",
                c => new
                    {
                        RSProduct_Id = c.Int(nullable: false),
                        RSOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RSProduct_Id, t.RSOrder_Id })
                .ForeignKey("dbo.RSProducts", t => t.RSProduct_Id, cascadeDelete: true)
                .ForeignKey("dbo.RSOrders", t => t.RSOrder_Id, cascadeDelete: true)
                .Index(t => t.RSProduct_Id)
                .Index(t => t.RSOrder_Id);
            
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
            DropForeignKey("dbo.RSShifts", "User_Id", "dbo.RSUsers");
            DropForeignKey("dbo.RSOrders", "Waiter_Id", "dbo.RSUsers");
            DropForeignKey("dbo.RSStockRSProducts", "RSProduct_Id", "dbo.RSProducts");
            DropForeignKey("dbo.RSStockRSProducts", "RSStock_Id", "dbo.RSStocks");
            DropForeignKey("dbo.RSProductRSOrders", "RSOrder_Id", "dbo.RSOrders");
            DropForeignKey("dbo.RSProductRSOrders", "RSProduct_Id", "dbo.RSProducts");
            DropForeignKey("dbo.RSOrderRSDiscounts", "RSDiscount_Id", "dbo.RSDiscounts");
            DropForeignKey("dbo.RSOrderRSDiscounts", "RSOrder_Id", "dbo.RSOrders");
            DropForeignKey("dbo.RSDiscounts", "Author_Id", "dbo.RSUsers");
            DropIndex("dbo.RSShifts", new[] { "User_Id" });
            DropIndex("dbo.RSOrders", new[] { "Waiter_Id" });
            DropIndex("dbo.RSStockRSProducts", new[] { "RSProduct_Id" });
            DropIndex("dbo.RSStockRSProducts", new[] { "RSStock_Id" });
            DropIndex("dbo.RSProductRSOrders", new[] { "RSOrder_Id" });
            DropIndex("dbo.RSProductRSOrders", new[] { "RSProduct_Id" });
            DropIndex("dbo.RSOrderRSDiscounts", new[] { "RSDiscount_Id" });
            DropIndex("dbo.RSOrderRSDiscounts", new[] { "RSOrder_Id" });
            DropIndex("dbo.RSDiscounts", new[] { "Author_Id" });
            DropTable("dbo.RSStockRSProducts");
            DropTable("dbo.RSProductRSOrders");
            DropTable("dbo.RSOrderRSDiscounts");
            DropTable("dbo.RSStyles");
            DropTable("dbo.RSShifts");
            DropTable("dbo.RSStocks");
            DropTable("dbo.RSProducts");
            DropTable("dbo.RSOrders");
            DropTable("dbo.RSUsers");
            DropTable("dbo.RSDiscounts");
        }
    }
}
