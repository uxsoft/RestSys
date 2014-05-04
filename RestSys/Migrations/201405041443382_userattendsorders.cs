namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userattendsorders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RSUsers", "RSOrder_Id", "dbo.RSOrders");
            DropIndex("dbo.RSUsers", new[] { "RSOrder_Id" });
            CreateTable(
                "dbo.RSUserRSOrders",
                c => new
                    {
                        RSUser_Id = c.Int(nullable: false),
                        RSOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RSUser_Id, t.RSOrder_Id })
                .ForeignKey("dbo.RSUsers", t => t.RSUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.RSOrders", t => t.RSOrder_Id, cascadeDelete: true)
                .Index(t => t.RSUser_Id)
                .Index(t => t.RSOrder_Id);
            
            DropColumn("dbo.RSUsers", "RSOrder_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RSUsers", "RSOrder_Id", c => c.Int());
            DropForeignKey("dbo.RSUserRSOrders", "RSOrder_Id", "dbo.RSOrders");
            DropForeignKey("dbo.RSUserRSOrders", "RSUser_Id", "dbo.RSUsers");
            DropIndex("dbo.RSUserRSOrders", new[] { "RSOrder_Id" });
            DropIndex("dbo.RSUserRSOrders", new[] { "RSUser_Id" });
            DropTable("dbo.RSUserRSOrders");
            CreateIndex("dbo.RSUsers", "RSOrder_Id");
            AddForeignKey("dbo.RSUsers", "RSOrder_Id", "dbo.RSOrders", "Id");
        }
    }
}
