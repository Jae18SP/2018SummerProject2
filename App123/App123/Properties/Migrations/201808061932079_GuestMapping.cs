namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuestId1 = c.Int(nullable: false),
                        GuestId2 = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GuestMappings");
        }
    }
}
