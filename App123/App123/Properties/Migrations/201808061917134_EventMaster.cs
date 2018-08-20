namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventMaster : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventMasters",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        TableId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DOC = c.DateTime(nullable: false),
                        DOM = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventMasters");
        }
    }
}
