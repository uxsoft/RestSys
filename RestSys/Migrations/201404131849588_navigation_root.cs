namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navigation_root : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSNavigationItems", "IsRoot", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSNavigationItems", "IsRoot");
        }
    }
}
