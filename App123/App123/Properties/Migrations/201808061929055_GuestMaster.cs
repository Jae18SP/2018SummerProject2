namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestMaster : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestMasters",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        GuestName = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GuestMasters");
        }
    }
}
