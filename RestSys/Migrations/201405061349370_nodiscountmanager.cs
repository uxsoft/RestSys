namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nodiscountmanager : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RSOrders", "Title", c => c.String(nullable: false));
            DropColumn("dbo.RSUsers", "IsDiscountManager");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RSUsers", "IsDiscountManager", c => c.Boolean(nullable: false));
            AlterColumn("dbo.RSOrders", "Title", c => c.String());
        }
    }
}
