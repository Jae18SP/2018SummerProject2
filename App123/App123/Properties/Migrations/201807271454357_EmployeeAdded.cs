namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeMasters",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                        EmployeeEmail = c.String(nullable: false),
                        EmployeePassword = c.String(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        DOC = c.DateTime(nullable: false),
                        DOM = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeMasters");
        }
    }
}
