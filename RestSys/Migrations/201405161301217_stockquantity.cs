namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockquantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSStocks", "Quantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSStocks", "Quantity");
        }
    }
}
