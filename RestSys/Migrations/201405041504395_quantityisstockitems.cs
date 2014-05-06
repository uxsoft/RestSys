namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quantityisstockitems : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RSStocks", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RSStocks", "Quantity", c => c.Double(nullable: false));
        }
    }
}
