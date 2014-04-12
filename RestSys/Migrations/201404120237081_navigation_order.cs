namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navigation_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSNavigationItems", "ChildrenOrder", c => c.String());
            DropColumn("dbo.RSNavigationItems", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RSNavigationItems", "Position", c => c.Int(nullable: false));
            DropColumn("dbo.RSNavigationItems", "ChildrenOrder");
        }
    }
}
