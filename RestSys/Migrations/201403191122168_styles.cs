namespace RestSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class styles : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RSStyles");
        }
    }
}
