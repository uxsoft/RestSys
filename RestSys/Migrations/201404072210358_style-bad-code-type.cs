namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stylebadcodetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RSStyles", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RSStyles", "Code", c => c.Int(nullable: false));
        }
    }
}
