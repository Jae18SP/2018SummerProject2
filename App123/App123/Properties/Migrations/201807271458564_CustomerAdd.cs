namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerMasters",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        CustomerPhone = c.String(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        DOC = c.DateTime(nullable: false),
                        DOM = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerMasters");
        }
    }
}
