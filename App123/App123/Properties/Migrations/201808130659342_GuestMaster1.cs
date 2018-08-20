namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuestMaster1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GuestMasters", "GuestName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GuestMasters", "GuestName", c => c.Int(nullable: false));
        }
    }
}
