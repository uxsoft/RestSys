namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navigation_color : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSNavigationItems", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSNavigationItems", "Color");
        }
    }
}
