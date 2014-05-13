namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderitempricemigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSOrderItems", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSOrderItems", "Price");
        }
    }
}
